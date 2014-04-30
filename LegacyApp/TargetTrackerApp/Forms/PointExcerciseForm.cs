using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using TargetTracker;
using TargetTrackerApp.BL;

namespace TargetTrackerApp.Forms
{
    // ReSharper disable LocalizableElement
    public partial class PointExcerciseForm : Form
    {
        private readonly List<CameraSnapshotDescriptor> snapshots;
        private readonly List<VideoCaptureDevice> videoSources = new List<VideoCaptureDevice>();

        #region Переменные состояния
        private bool captureInProcess;
        private int curIteration;
        private int lastCamIndex;
        private DateTime nextStart, nextEnd;
        private PointExcerciseResults results;
        #endregion        

        public PointExcerciseForm()
        {
            InitializeComponent();
        }

        public PointExcerciseForm(List<CameraSnapshotDescriptor> snapshots)
        {
            InitializeComponent();
            this.snapshots = snapshots;
            SetupFrameEventHandlers();
        }

        private void BtnStartStopClick(object sender, EventArgs e)
        {
            if (!captureInProcess)
            {
                InitSettings();
                SaveSettings();
                // старт слежения
                if (!StartCapture()) return;

                btnStartStop.Text = "Остановить";
                SetStatusLabelSafe("старт...");
                curIteration = 1;
                lastCamIndex = 0;
                results = new PointExcerciseResults();
                spotCoordsByTime = new List<PointCoordsByTime>[snapshots.Count];
                for (var i = 0; i < snapshots.Count; i++)
                    spotCoordsByTime[i] = new List<PointCoordsByTime>();
                // расчет времени
                CalculateNextStart();
                // таймер задания
                timerExcercise.Enabled = true;
                Speaker.Instance.SayAsynch(SpokenWord.Старт);
                return;
            }

            // остановить упражнение
            StopExcercise();
        }

        private void StopExcercise()
        {
            timerExcercise.Enabled = false;
            Speaker.Instance.SayAsynch(SpokenWord.Финал);
            CloseAllSources();
            btnStartStop.Text = "Старт!";
            SetStatusLabelSafe("завершено");
            // показать результаты
            var resultsStr = GetResultsString(ProcessResults());
            tbResults.AppendText(Environment.NewLine + resultsStr);
        }

        private bool StartCapture()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("Активные видеоустройства не найдены");
                return false;
            }

            var camIndex = 0;
            foreach (var shot in snapshots)
            {                
                var monikerStr = string.Empty;
                var camName = shot.cameraName;
                foreach (var dev in videoDevices.Cast<FilterInfo>().Where(dev => dev.Name ==
                    camName))
                {
                    monikerStr = dev.MonikerString;
                    break;
                }
                if (string.IsNullOrEmpty(monikerStr))
                {
                    CloseAllSources();
                    MessageBox.Show(string.Format("Видеоустройство \"{0}\" не найдено (возможно, отключено)",
                        camName));
                    return false;
                }
                var videoSource = new VideoCaptureDevice(monikerStr);
                videoSource.NewFrame += camEventHandlers[camIndex++];
                videoSource.DesiredFrameSize = shot.resolution;
                videoSource.Start();
                videoSources.Add(videoSource);                
            }
            captureInProcess = true;
            return true;
        }

        private void CloseAllSources()
        {
            for (var i = 0; i < videoSources.Count; i++)
            {
                var videoSource = videoSources[i];
                if (videoSource == null) continue;
                if (videoSource.IsRunning)
                    videoSource.SignalToStop();
                videoSources[i] = null;
            }
            captureInProcess = false;
            SetStatusLabelSafe("завершено");
        }

        private void TimerExcerciseTick(object sender, EventArgs e)
        {
            if (!captureInProcess)
            {// упражнение остановлено
                timerExcercise.Enabled = false;
                StopExcercise();
                return;
            }

            if (DateTime.Now >= nextEnd)
            {                
                // дать сигнал
                //Speaker.Instance.SayAsynch(SpokenWord.Стоп);
                // если упражнение завершено...
                curIteration++;
                SetStatusLabelSafe(string.Format("цель {0}", curIteration));
                if (curIteration <= iterationCount)
                    Speaker.Instance.SayAsynch(SpokenWord.Стоп);
                
                // упражнение завершено
                if (curIteration > iterationCount)
                {
                    results.CompleteTarget();
                    StopExcercise();
                    return;
                }
                // работа с мишенью завершена
                results.CompleteTarget();
                CalculateNextStart();
                return;
            }

            if (DateTime.Now >= nextStart)
            {
                nextStart = nextEnd.AddMinutes(1);
                // дать сигнал - выбор камеры
                var camIndex = 0;
                if (randomCamera)
                    lastCamIndex = randomGenr.Next(snapshots.Count);
                else
                {
                    lastCamIndex++;
                    if (lastCamIndex >= snapshots.Count) lastCamIndex = 0;
                    camIndex = lastCamIndex;
                }
                Speaker.Instance.SayAsynch(camIndex == 0 ? SpokenWord.Камера1 : camIndex == 1 ?
                    SpokenWord.Камера2 : camIndex == 2 ? SpokenWord.Камера3 : SpokenWord.Камера4);
                // записать в результаты событие - временная метка - выбор камеры (мишени)
                results.SelectTarget(camIndex);

                // уточнить время окончания работы с мишенью
                nextEnd = DateTime.Now.AddSeconds(GetValueInInterval(timeToHold));
            }
        }

        private void PointExcerciseFormLoad(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
    // ReSharper restore LocalizableElement
}
