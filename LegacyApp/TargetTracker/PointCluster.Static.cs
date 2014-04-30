using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TargetTracker
{
    public partial class PointCluster
    {
        #region Статические функции
        public static PointCluster FindLargestCluster(List<PointCluster> clusters)
        {
            if (clusters.Count == 0) return null;
            var max = clusters[0];
            for (var i = 1; i < clusters.Count; i++)
            {
                if (clusters[i].points.Count > max.points.Count)
                    max = clusters[i];
            }
            return max;
        }

        public static List<PointCluster> FindClusters(Bitmap bmp,
            LazerSpot spotParams)
        {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            try
            {
                IntPtr ptr = bmpData.Scan0;
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                var rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                return FindClusters(spotParams, rgbValues,
                    bmp.Width, bmp.Height, bmpData.Stride);
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }
        }

        private static List<PointCluster> FindClusters(LazerSpot spotParams,
            byte[] rgbValues, int w, int h, int stride)
        {
            var clusters = new List<ScanCluster>();
            List<ScanLine> curLines = null;
            for (var y = 0; y < h; y++)
            {
                ScanLine curLine = null;
                curLines = new List<ScanLine>();
                
                for (var x = 0; x < w; x++)
                {
                    var ind = x * 3 + y * stride;
                    var red = rgbValues[ind];
                    var green = rgbValues[ind + 1];
                    var blue = rgbValues[ind + 2];
                    var delta = (red - spotParams.patRed) * (red - spotParams.patRed) + (green - spotParams.patGreen) * (green - spotParams.patGreen) +
                                (blue - spotParams.patBlue) * (blue - spotParams.patBlue);
                    var isIn = delta < spotParams.squareDev;                    
                    if (isIn)
                    {
                        if (curLine == null) 
                            curLine = new ScanLine { y = y, scans = new List<Point> { new Point(x, x) } };
                        else curLine.scans[0] = new Point(curLine.scans[0].X, x);
                    }
                    else
                    if (curLine != null)
                    {
                        curLines.Add(curLine);
                        curLine = null;
                    }
                }
                if (curLine != null) curLines.Add(curLine);
                if (curLines.Count == 0) continue;
                MergeClusters(curLines, clusters);
            }
            if (curLines != null && curLines.Count > 0) MergeClusters(curLines, clusters);
            return clusters.Select(c => c.ToPointCluster()).ToList();
        }

        private static void MergeClusters(List<ScanLine> curLines, List<ScanCluster> clusters)
        {
            var curClusters = new List<ScanCluster>();
            foreach (var line in curLines)
                curClusters.Add(new ScanCluster { lines = new List<ScanLine> { line }});
            // объединить линии в кластеры                
            foreach (var curCluster in curClusters)            
            {
                for (var i = 0; i < clusters.Count; i++)
                {
                    var cluster = clusters[i];
                    if (cluster.AreStick(curCluster))
                    {
                        curCluster.ConsumeCluster(cluster);                        
                        clusters.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
            }
            clusters.AddRange(curClusters);
        }

        #endregion
    }

    class ScanLine
    {
        public int y;
        public List<Point> scans;
        public void Consume(ScanLine line)
        {
            scans = scans.Union(line.scans).OrderBy(s => s.X).ToList();            
        }
    }

    class ScanCluster
    {
        public List<ScanLine> lines = new List<ScanLine>();
        
        public bool AreStick(ScanCluster cluster)
        {
            // cluster is beneath this
            var lastLine = cluster.lines[cluster.lines.Count - 1];
            var ownScan = lines[lines.Count - 1].y == lastLine.y - 1
                              ? lines[lines.Count - 1].scans
                              : lines.Count > 1 && lines[lines.Count - 2].y == lastLine.y - 1
                                    ? lines[lines.Count - 2].scans : null;
            if (ownScan != null)
            {
                foreach (var span in ownScan)
                {
                    var spanX = span.X;
                    var spanY = span.Y;
                    if (lastLine.scans.Any(s => (s.X >= spanX && s.X <= spanY) ||
                        (s.Y >= spanX && s.Y <= spanY))) return true;
                    if (lastLine.scans.Any(s => (spanX >= s.X && spanY <= s.X) ||
                        (spanX >= s.Y && spanY <= s.Y))) return true;
                }
            }
            return false;
        }

        public void ConsumeCluster(ScanCluster cluster)
        {
            var thisStart = lines[0].y;
            var thisEnd = lines[lines.Count - 1].y;
            var clusterStart = cluster.lines[0].y;
            var clusterEnd = cluster.lines[cluster.lines.Count - 1].y;

            var scans = new List<ScanLine>();
            var min = Math.Min(thisStart, clusterStart);
            var max = Math.Max(thisEnd, clusterEnd);
            for (var i = min; i <= max; i++)
            {
                if (i >= thisStart && i <= thisEnd && (i < clusterStart || i > clusterEnd))
                {
                    scans.Add(lines[i - thisStart]);
                    continue;
                }
                if (i >= clusterStart && i <= clusterEnd && (i < thisStart || i > thisEnd))
                {
                    scans.Add(cluster.lines[i - clusterStart]);
                    continue;
                }
                // склеить
                lines[i - thisStart].Consume(cluster.lines[i - clusterStart]);
                scans.Add(lines[i - thisStart]);
            }
            lines = scans;
        }
    
        public PointCluster ToPointCluster()
        {
            var cluster = new PointCluster();
            foreach (var line in lines)
            {
                foreach (var span in line.scans)
                {
                    for (var x = span.X; x <= span.Y; x++)
                        cluster.points.Add(new Point(x, line.y));
                }
            }
            return cluster;
        }
    }
}