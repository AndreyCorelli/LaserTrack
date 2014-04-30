using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using AForge.Video;
using AForge.Video.DirectShow;
using TargetTracker;

namespace TargetTrackerApp.Forms
{
    public partial class CalibrationForm : Form
    {
        private List<BaseTarget> targets;
        private readonly List<CameraSnapshotDescriptor> snapshots = new List<CameraSnapshotDescriptor>();
        private VideoCaptureDevice videoSource;
        private bool captureInProcess;
        private const int ImageIndexStart = 0, ImageIndexStop = 1;
        public List<CameraSnapshotDescriptor> CameraSnapshotDescriptors
        {
            get { return snapshots; }
        }
        private CameraSnapshotDescriptor curDescriptor;
        private volatile bool showSpot;

        public CalibrationForm()
        {
            InitializeComponent();
        }

        private void CalibrationFormLoad(object sender, EventArgs e)
        {
            targets = TargetStorage.Instance.LoadTargets();
        }

        private void BtnAddCameraClick(object sender, EventArgs e)
        {
            if (captureInProcess) return;
            // открыть диалог выбора камеры и мишени
            // в диалоге запретить уже выбранные камеры
            var camUsed = snapshots.Select(s => s.cameraName).ToList();
            var dlg = new PickCameraDialog(camUsed);
            if (dlg.ShowDialog() == DialogResult.Cancel) return;

            const int markerPadding = 40;
            
            var snap = new CameraSnapshotDescriptor
                           {
                               cameraName = dlg.SelectedCamera,
                               spotDescriptor = new LazerSpot(panelSpot.BackColor, tbSpotTolerance.Text.ToInt(),
                                                              tbPixelsInSpot.Text.ToInt(), tbMaxSizeOfDot.Text.ToInt()),
                               resolution = dlg.FrameSize,
                               ptCentre = new Point(dlg.FrameSize.Width/2, dlg.FrameSize.Height/2),
                               target = targets.First(t => t.name == dlg.SelectedTarget),
                               points = new [] 
                               {                               
                                   new Point(markerPadding, markerPadding),
                                   new Point(dlg.FrameSize.Width - markerPadding, markerPadding),
                                   new Point(dlg.FrameSize.Width - markerPadding,
                                                                dlg.FrameSize.Height - markerPadding),
                                   new Point(markerPadding, dlg.FrameSize.Height - markerPadding)
                               }
                           };
            snap.RecalculateCentre();
            snapshots.Add(snap);
            lbCameras.Items.Add(snap);
            lbCameras.SelectedItem = snap;
            pbFrame.Size = dlg.FrameSize;
        }

        private void BtnRemoveCameraClick(object sender, EventArgs e)
        {
            if (captureInProcess) return;
            // убрать настройки камеры из списка
            if (lbCameras.SelectedIndex < 0) return;
            var selIndex = lbCameras.SelectedIndex;
            var snapshot = (CameraSnapshotDescriptor) lbCameras.SelectedItem;
            snapshots.Remove(snapshot);
            lbCameras.Items.RemoveAt(selIndex);
            if (selIndex < lbCameras.Items.Count)
                lbCameras.SelectedIndex = selIndex;
        }

        private void BtnStartStopClick(object sender, EventArgs e)
        {
            // начать/остановить захват
            if (!captureInProcess)
            {
                captureInProcess = StartCapture();
                if (captureInProcess) btnStartStop.ImageIndex = ImageIndexStop;
                return;
            }
            CloseVideoSource();
            captureInProcess = false;
            btnStartStop.ImageIndex = ImageIndexStart;
        }

        private void OnNewFrame(object sender, NewFrameEventArgs e)
        {
            if (IsDisposed) return;
            // нарисовать картинку и мишень поверх
            // производительность здесь несущественна
            var img = (Bitmap)e.Frame.Clone();
            if (curDescriptor == null) return;
            Point? spot = !showSpot ? null : curDescriptor.GetSpotPosition(img);
            curDescriptor.DrawOnImage(img, spot);
            var score = spot == null ? 0.0 : curDescriptor.GetMarkByScreenCoords(spot.Value);
            ShowScoreSafe(score);
            //if (spot != null) ShowRealCoordsPointSafe(curDescriptor.PixelsToReal(spot.Value));
            //else ShowRealCoordsPointSafe(null);
            // нарисовать точку
            pbFrame.Image = img;
        }

        private bool StartCapture()
        {
            if (lbCameras.SelectedIndex < 0) return false;
            var curSnap = (CameraSnapshotDescriptor) lbCameras.SelectedItem;
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0) return false;
            var monikerStr = string.Empty;
            foreach (var dev in videoDevices.Cast<FilterInfo>().Where(dev => dev.Name == curSnap.cameraName))
            {
                monikerStr = dev.MonikerString;
                break;
            }
            if (string.IsNullOrEmpty(monikerStr)) return false;
            CloseVideoSource();
            videoSource = new VideoCaptureDevice(monikerStr);
            videoSource.NewFrame += OnNewFrame;            
            videoSource.DesiredFrameSize = curSnap.resolution;
            videoSource.Start();
            return true;
        }

        private void CloseVideoSource()
        {
            if (videoSource == null) return;
            if (videoSource.IsRunning)                
                videoSource.SignalToStop();                                    
            videoSource = null;
        }

        private void CalibrationFormFormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();
        }

        private void PbFrameMouseClick(object sender, MouseEventArgs e)
        {
            if (lbCameras.SelectedIndex < 0) return;
            var curSnap = (CameraSnapshotDescriptor)lbCameras.SelectedItem;            
            var x = e.X;
            var y = e.Y;
            // в зависимости от режима - переставить маркер либо
            // выбрать цвет точки
            if (rbModeMarker.Checked)
            {
                // переставить маркер
                // найти ближайший
                int nearestIndex = -1, leastRad = int.MaxValue;
                for (var i = 0; i < curSnap.points.Length; i++)
                {
                    var pt = curSnap.points[i];
                    var r = (x - pt.X)*(x - pt.X) + (y - pt.Y)*(y - pt.Y);
                    if (r < leastRad)
                    {
                        nearestIndex = i;
                        leastRad = r;
                    }
                }
                curSnap.points[nearestIndex] = new Point(x, y);
                curSnap.RecalculateCentre();
                return;
            }
            
            // определить цвет пятна
            if (captureInProcess) return;
            curSnap.spotDescriptor = new LazerSpot(((Bitmap) pbFrame.Image).GetPixel(x, y),
                tbSpotTolerance.Text.ToInt(), tbPixelsInSpot.Text.ToInt(), tbMaxSizeOfDot.Text.ToInt());
            panelSpot.BackColor = Color.FromArgb(curSnap.spotDescriptor.patRed, curSnap.spotDescriptor.patGreen,
                                                      curSnap.spotDescriptor.patBlue);
        }

        private void BtnAcceptClick(object sender, EventArgs e)
        {
            if (lbCameras.SelectedIndex < 0) return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LbCamerasSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCameras.SelectedIndex < 0) curDescriptor = null;
            curDescriptor = (CameraSnapshotDescriptor)lbCameras.SelectedItem;
        }

        private void CbShowSpotCheckedChanged(object sender, EventArgs e)
        {
            showSpot = cbShowSpot.Checked;
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #region Метка - текущее кол-во очков
        //private delegate void ShowRealCoordsPointDel(PointD? pt);
        //private void ShowRealCoordsPointUnsafe(PointD? pt)
        //{
        //    lblScore.Text = pt == null ? "-" : string.Format("{0:f1}; {1:f1}", pt.Value.X, pt.Value.Y);
        //}
        //private void ShowRealCoordsPointSafe(PointD? pt)
        //{
        //    if (IsDisposed) return;
        //    BeginInvoke(new ShowRealCoordsPointDel(ShowRealCoordsPointUnsafe), pt);
        //}
        private delegate void ShowScoreDel(double score);
        private void ShowScoreUnsafe(double score)
        {
            lblScore.Text = score == 0 ? "-" : score.ToString("f1");
        }
        private void ShowScoreSafe(double score)
        {
            if (IsDisposed) return;
            BeginInvoke(new ShowScoreDel(ShowScoreUnsafe), score);
        }
        #endregion
    }
}
