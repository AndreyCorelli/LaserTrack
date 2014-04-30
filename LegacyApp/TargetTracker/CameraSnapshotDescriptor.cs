using System.Drawing;
using System.Linq;

namespace TargetTracker
{
    public class CameraSnapshotDescriptor
    {
        public string cameraName;
        public BaseTarget target;
        /// <summary>
        /// точки привязки
        /// </summary>
        public Point[] points = new Point[4];
        public Point ptCentre;
        public Size resolution;
        public LazerSpot spotDescriptor;
        public double scaleX, scaleY;

        public override string ToString()
        {
            return cameraName;
        }

        public void RecalculateCentre()
        {
            ptCentre = new Point((int)points.Average(p => p.X), (int)points.Average(p => p.Y));
            scaleX = target.size.Width / (((points[1].X - points[0].X) + 
                (points[2].X - points[3].X)) * 0.5);
            scaleY = target.size.Height / (((points[3].Y - points[0].Y) + 
                (points[2].Y - points[1].Y)) * 0.5);
        }

        public void DrawOnImage(Bitmap img, Point? spotPos)
        {
            // нарисовать мишень
            if (ptCentre == default(Point)) return;
            using (var pen = new Pen(Color.Blue))
            using (var g = Graphics.FromImage(img))
            {                
                const int crossSz = 4;
                foreach (var mark in points)
                {
                    g.DrawLine(pen, mark.X, mark.Y + 2, mark.X, mark.Y + 2 + crossSz);
                    g.DrawLine(pen, mark.X, mark.Y - 2, mark.X, mark.Y - 2 - crossSz);
                    g.DrawLine(pen, mark.X - 2, mark.Y, mark.X - 2 - crossSz, mark.Y);
                    g.DrawLine(pen, mark.X + 2, mark.Y, mark.X + 2 + crossSz, mark.Y);
                }
                const int bigCrossSz = 6;
                g.DrawLine(pen, ptCentre.X, ptCentre.Y + 2, ptCentre.X, ptCentre.Y + 2 + bigCrossSz);
                g.DrawLine(pen, ptCentre.X, ptCentre.Y - 2, ptCentre.X, ptCentre.Y - 2 - bigCrossSz);
                g.DrawLine(pen, ptCentre.X - 2, ptCentre.Y, ptCentre.X - 2 - bigCrossSz, ptCentre.Y);
                g.DrawLine(pen, ptCentre.X + 2, ptCentre.Y, ptCentre.X + 2 + bigCrossSz, ptCentre.Y);
                
                if (spotPos.HasValue) DrawSpotPosition(g, spotPos.Value);
            }            
        }
    
        /// <summary>
        /// вернуть координаты точки или null, если точка не распознана
        /// возвращает самую удаленную точку кластера, если его размер больше заданного,
        /// либо центр кластера
        /// </summary>        
        public Point? GetSpotPosition(Bitmap img)
        {
            var clusters = PointCluster.FindClusters(img, spotDescriptor);
            if (clusters.Count == 0) return null;
            if (clusters.Count == 1)
            {// м.б. имеем выраженную точку
                int minX = int.MaxValue, maxX = int.MinValue;
                int minY = int.MaxValue, maxY = int.MinValue;
                foreach (var pt in clusters[0].points)
                {
                    if (pt.X < minX) minX = pt.X;
                    else if (pt.X > maxX) maxX = pt.X;
                    if (pt.Y < minY) minY = pt.Y;
                    else if (pt.Y > maxY) maxY = pt.Y;
                }
                int wd = maxX - minX, ht = maxY - minY;
                var sz = wd > ht ? wd : ht;
                if (sz <= spotDescriptor.maxPointsToConsiderDot)
                {// точка
                    int x = 0, y = 0;
                    foreach (var pt in clusters[0].points)
                    {
                        x += pt.X;
                        y += pt.Y;
                    }
                    return new Point(x / clusters[0].points.Count, y / clusters[0].points.Count);
                }
            }
            // имеем несколько кластеров либо один размазанный
            const int countClustersToGetCenter = 2;
            if (clusters.Count > 1)
                clusters = clusters.OrderByDescending(c => c.points.Count).ToList();
            Point near, far = new Point();
            double minDist, maxDist;
            Point? lastFarPoint = null;
            for (var i = 0; i < clusters.Count && i < countClustersToGetCenter; i++)
            {
                clusters[i].GetMinMaxDistance(ptCentre, out near, out far, out minDist, out maxDist);
                if (lastFarPoint != null)
                {
                    var v = new Point(far.X - lastFarPoint.Value.X, far.Y - lastFarPoint.Value.Y);
                    var scale = clusters[i].points.Count/(double)(clusters[i].points.Count + clusters[i - 1].points.Count);
                    far = new Point(lastFarPoint.Value.X + (int) (v.X*scale),
                                    lastFarPoint.Value.Y + (int) (v.Y*scale));
                }
                else lastFarPoint = far;
            }
            return far;
        }
    
        private static void DrawSpotPosition(Graphics g, Point spot)
        {
            const int crossSz = 8;
            using (var p1 = new Pen(Color.White))
            using (var p2 = new Pen(Color.Black))
            {
                g.DrawLine(p1, spot.X, spot.Y + 2, spot.X, spot.Y + 2 + crossSz);
                g.DrawLine(p1, spot.X, spot.Y - 2, spot.X, spot.Y - 2 - crossSz);
                g.DrawLine(p2, spot.X - 2, spot.Y, spot.X - 2 - crossSz, spot.Y);
                g.DrawLine(p2, spot.X + 2, spot.Y, spot.X + 2 + crossSz, spot.Y);
            }
        }
    
        public PointD PixelsToReal(Point pt)
        {
            var dx = pt.X - ptCentre.X;
            var dy = ptCentre.Y - pt.Y;
            return new PointD(dx * scaleX, dy * scaleY);
        }

        public double GetMarkByScreenCoords(Point pt)
        {
            var ptR = PixelsToReal(pt);
            return target.GetMark(ptR.X, ptR.Y);
        }
    }
}
