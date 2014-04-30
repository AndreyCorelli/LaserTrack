using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TargetTracker;
using TargetTrackerApp.Forms;

namespace TargetTrackerApp
{
    public partial class MainForm : Form
    {
        private List<CameraSnapshotDescriptor> snapshots = new List<CameraSnapshotDescriptor>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnCalibrateClick(object sender, EventArgs e)
        {
            var dlg = new CalibrationForm();
            if (dlg.ShowDialog() != DialogResult.OK) return;
            if (dlg.CameraSnapshotDescriptors.Count > 0)
                snapshots = dlg.CameraSnapshotDescriptors;
            if (snapshots.Count > 0)
            {
                btnStartExPoint.Enabled = true;
            }
        }

        private void BtnStartExPointClick(object sender, EventArgs e)
        {
            // запустить приложение - отработка наведения
            var dlg = new PointExcerciseForm(snapshots);
            dlg.ShowDialog();
        }
    }
}
