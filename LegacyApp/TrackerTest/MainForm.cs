using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TargetTracker;

namespace TrackerTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnTestClick(object sender, EventArgs e)
        {
            var pathSrc = ExecutablePath.ExecPath + "\\src";
            var pathDest = ExecutablePath.ExecPath + "\\dest";
            var colorBytes = tbColor.Text.ToIntArrayUniform();
            var spotParams = new LazerSpot(Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2]),
                                           tbDeviation.Text.ToInt(), tbMinSize.Text.ToInt(), tbMaxSize.Text.ToInt());

            var replacementColorMax = Color.Blue;
            var replacementColor = Color.Green;

            foreach (var fileName in Directory.GetFiles(pathSrc, "*.*"))
            {
                var img = (Bitmap)Image.FromFile(fileName);
                var clusters = PointCluster.FindClusters(img, spotParams);
                Logger.InfoFormat("\"{0}\": {1} кластеров", fileName, clusters.Count);
                if (clusters.Count == 0) continue;
                foreach (var c in clusters)
                {
                    foreach (var pt in c.points)
                    {
                        if (pt.X < img.Width && pt.Y < img.Height)
                            img.SetPixel(pt.X, pt.Y, replacementColor);
                    }
                }
                var cluster = PointCluster.FindLargestCluster(clusters);                
                foreach (var pt in cluster.points)
                {
                    if (pt.X < img.Width && pt.Y < img.Height)
                        img.SetPixel(pt.X, pt.Y, replacementColorMax);
                }
                var outputFileName = string.Format("{0}\\{1}", pathDest, Path.GetFileName(fileName));
                img.Save(outputFileName, ImageFormat.Png);
            }
            MessageBox.Show("Готово");
        }
    }
}
