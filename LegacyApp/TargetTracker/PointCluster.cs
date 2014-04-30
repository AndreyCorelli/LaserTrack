using System;
using System.Collections.Generic;
using System.Drawing;

namespace TargetTracker
{
    public partial class PointCluster
    {
        public List<Point> points;        

        public PointCluster()
        {
            points = new List<Point>();
        }

        public PointCluster(Point point)
        {
            points = new List<Point> { point };
        }

        public void GetMinMaxDistance(Point center, out Point near, out Point far, out double minDist, out double maxDist)
        {
            var min = int.MaxValue;
            var max = int.MinValue;
            near = new Point();
            far = new Point();

            foreach (var p in points)
            {
                var r = (center.X - p.X)*(center.X - p.X) + (center.Y - p.Y)*(center.Y - p.Y);
                if (r < min)
                {
                    min = r;
                    near = p;
                }
                if (r > max)
                {
                    max = r;
                    far = p;
                } 
            }
            minDist = Math.Sqrt(min);
            maxDist = Math.Sqrt(max);
        }

        #region Скелетизация
        /// <param name="maxPointsCountToConsiderDot">если в кластере меньше указанного точек, он считается пятном (не сложной траекторией)</param>
        /// <param name="pointsBetweenNodes">если значение > 0 - строить ломаную с вершинами через каждые N точек</param>
        /// <param name="waveArray">массив для волнового прохода, 
        /// размерность совпадает с размерностью картинки</param>
        /// <param name="w">размер волнового массива (размер картинки)</param>
        /// <param name="h">размер волнового массива (размер картинки)</param>
        /// <returns></returns>
        public List<Point> GetTrack(int maxPointsCountToConsiderDot, int pointsBetweenNodes,
            int[] waveArray, int w, int h)
        {
            if (points.Count <= maxPointsCountToConsiderDot)
            {
                int sumX = 0, sumY = 0;
                foreach (var pt in points)
                {
                    sumX += pt.X;
                    sumY += pt.Y;
                }
                return new List<Point> { new Point(sumX / points.Count, sumY / points.Count) };
            }
            // искать крайние точки кластера
            // пометить кластер в волновом массиве (точка кластера = 1, прочие = 0)
            foreach (var pt in points)
                waveArray[pt.X + pt.Y * w] = 1;
            // получить самую удаленную точку от выбранной (с индексом 0)
            int maxWeight;
            var lastPt = CoverWaveArray(waveArray, points[0], w, h, 1, out maxWeight);
            // пройти в обратном направлении с отрицательным фронтом
            var firstPt = CoverWaveArray(waveArray, lastPt, w, h, -1, out maxWeight);
            // пройти от firstPt (фронт = -1) до lastPt, через каждые N точек добавляя по узлу в список
            var pivots = pointsBetweenNodes > 0 ? GetRouteInWaveArray(waveArray, firstPt, lastPt, w, h, pointsBetweenNodes)
                : new List<Point> { lastPt, firstPt };
            // очистить массив waveArray
            foreach (var pt in points) waveArray[pt.X + pt.Y * w] = 0;
            return pivots;
        }

        /// <summary>
        /// пройти по волновому массиву от точки start
        /// свободными считаются элементы, не равные 0
        /// вернуть координаты одного из узлов с наибольшим значением - фронт волны
        /// (последнего в итерации)
        /// direction - 1 или -1
        /// </summary>        
        private Point CoverWaveArray(int[] waveArray, Point start,
            int w, int h, int direction, out int maxWeight)
        {
            var front = direction > 0 ? 2 : -1;
            waveArray[start.X + start.Y * w] = front;
            maxWeight = 0;
            var lastPoint = new Point();

            while (true)
            {
                var cellsAdded = false;

                foreach (var pt in points)
                {
                    var ptValue = waveArray[pt.X + pt.Y * w];
                    if ((ptValue > 1 && direction > 0) ||
                        (ptValue < 0 && direction < 0)) continue;
                    int? leastFront = null;
                    if (pt.X > 0)
                    {
                        var curVal = waveArray[pt.X - 1 + pt.Y * w];
                        if (direction < 0)
                        {
                            if (curVal < 0) leastFront = curVal;
                        }
                        else
                            if (curVal > 1) leastFront = curVal;
                    }
                    if (pt.X < w - 1)
                    {
                        var curVal = waveArray[pt.X + 1 + pt.Y * w];
                        if (direction < 0)
                        {
                            if (curVal < 0 && (leastFront == null || curVal > leastFront.Value))
                                leastFront = curVal;
                        }
                        else
                            if (curVal > 1 && (leastFront == null || curVal < leastFront.Value))
                                leastFront = curVal;
                    }
                    if (pt.Y > 0)
                    {
                        var curVal = waveArray[pt.X + (pt.Y - 1) * w];
                        if (direction < 0)
                        {
                            if (curVal < 0 && (leastFront == null || curVal > leastFront.Value))
                                leastFront = curVal;
                        }
                        else
                            if (curVal > 1 && (leastFront == null || curVal < leastFront.Value))
                                leastFront = curVal;
                    }
                    if (pt.Y < h - 1)
                    {
                        var curVal = waveArray[pt.X + (pt.Y + 1) * w];
                        if (direction < 0)
                        {
                            if (curVal < 0 && (leastFront == null || curVal > leastFront.Value))
                                leastFront = curVal;
                        }
                        else
                            if (curVal > 1 && (leastFront == null || curVal < leastFront.Value))
                                leastFront = curVal;
                    }
                    if (leastFront != null)
                    {
                        cellsAdded = true;
                        var weight = leastFront.Value + direction;
                        waveArray[pt.X + pt.Y * w] = weight;

                        if ((direction > 0 && weight > maxWeight) ||
                            (direction < 0 && weight < maxWeight))
                        {
                            lastPoint = pt;
                            maxWeight = weight;
                        }
                    }
                }// end of foreach (var pt...
                if (!cellsAdded) return lastPoint;
            }// end of while (true)...
        }

        private List<Point> GetRouteInWaveArray(int[] waveArray, Point start, Point end,
            int w, int h, int stepsBetweenNode)
        {
            // массив заполнен 0 (вне кластера) и отрицательными числами
            // в точке start фронт равен -1, в остальных точках - убывает с шагом 1 (-2, -3 ...)
            // от точки end перемещаемся в точку start по возрастанию фронта
            var nodes = new List<Point> { start };
            var curPt = start;
            var numSteps = 0;
            while (curPt != end)
            {
                var curFront = waveArray[curPt.X + curPt.Y * w] + 1;
                if (curPt.X > 0 && waveArray[curPt.X - 1 + curPt.Y * w] == curFront)
                    curPt = new Point(curPt.X - 1, curPt.Y);
                else if (curPt.X < w - 1 && waveArray[curPt.X + 1 + curPt.Y * w] == curFront)
                    curPt = new Point(curPt.X + 1, curPt.Y);
                else if (curPt.Y < h - 1 && waveArray[curPt.X + (curPt.Y + 1) * w] == curFront)
                    curPt = new Point(curPt.X, curPt.Y + 1);
                else if (curPt.Y > 0 && waveArray[curPt.X + (curPt.Y - 1) * w] == curFront)
                    curPt = new Point(curPt.X, curPt.Y - 1);
                numSteps++;
                if (numSteps >= stepsBetweenNode)
                {
                    numSteps = 0;
                    nodes.Add(curPt);
                }
            }
            if (numSteps < stepsBetweenNode / 2 && nodes.Count > 1) nodes.RemoveAt(nodes.Count - 1);
            nodes.Add(end);
            return nodes;
        }
        #endregion
    }
}
