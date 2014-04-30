using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using TargetTracker;

namespace TargetTrackerApp.Forms
{
    public partial class PickCameraDialog : Form
    {
        private readonly List<string> usedCameras;
        public string SelectedCamera
        {
            get { return (string) cbCameras.SelectedItem; }
        }
        public string SelectedTarget
        {
            get { return (string)cbTargets.SelectedItem; }
        }

        public Size FrameSize
        {
            get
            {
                var sizeParts = tbSize.Text.ToIntArrayUniform();
                return new Size(sizeParts[0], sizeParts[1]);
            }
        }
        
        public PickCameraDialog()
        {
            InitializeComponent();
        }

        public PickCameraDialog(List<string> usedCameras)
        {
            InitializeComponent();
            this.usedCameras = usedCameras;
        }

        private void PickCameraDialogLoad(object sender, EventArgs e)
        {
            // показать доступные камеры и мишени
            var cameras = GetCamList().Except(usedCameras);
            if (cameras.Count() == 0) return;
            foreach (var cam in cameras)
                cbCameras.Items.Add(cam);
            cbCameras.SelectedIndex = 0;
            // мишени
            var targets = TargetStorage.Instance.LoadTargets();
            foreach (var target in targets)
            {
                cbTargets.Items.Add(target.name);
            }
            cbTargets.SelectedIndex = 0;
        }

        private static List<string> GetCamList()
        {
            try
            {
                var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                    return new List<string>();
                return (from FilterInfo videoDevice in videoDevices select videoDevice.Name).ToList();
            }
            catch (ApplicationException)
            {
                return new List<string>();
            }
        }

        private void BtnAcceptClick(object sender, EventArgs e)
        {
            if (cbCameras.SelectedIndex < 0 || cbTargets.SelectedIndex < 0) return;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
