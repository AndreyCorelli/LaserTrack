using System;
using System.Collections.Generic;
using System.Drawing;
using AForge.Video;
using TargetTrackerApp.BL;

namespace TargetTrackerApp.Forms
{
    public partial class PointExcerciseForm
    {
        /// <summary>
        /// индекс в массиве - индекс камеры
        /// </summary>
        private List<PointCoordsByTime>[] spotCoordsByTime;
        private readonly List<NewFrameEventHandler> camEventHandlers = new List<NewFrameEventHandler>();

        private void OnNewFrame(NewFrameEventArgs e, int cameraIndex)
        {
            var img = (Bitmap)e.Frame.Clone();
            // найти координаты пятна и сохранить в лог вида камера/время/координаты
            var camDescriptor = snapshots[cameraIndex];
            var spot = camDescriptor.GetSpotPosition(img);
            // для текущей камеры сохранить координаты лазерной точки
            spotCoordsByTime[cameraIndex].Add(new PointCoordsByTime { Time = DateTime.Now, Coords = spot });
        }

        private void OnNewFrameCam1(object sender, NewFrameEventArgs e)
        {
            OnNewFrame(e, 0);
        }
        private void OnNewFrameCam2(object sender, NewFrameEventArgs e)
        {
            OnNewFrame(e, 1);
        }
        private void OnNewFrameCam3(object sender, NewFrameEventArgs e)
        {
            OnNewFrame(e, 2);
        }
        private void OnNewFrameCam4(object sender, NewFrameEventArgs e)
        {
            OnNewFrame(e, 3);
        }

        private void SetupFrameEventHandlers()
        {
            camEventHandlers.Add(OnNewFrameCam1);
            camEventHandlers.Add(OnNewFrameCam2);
            camEventHandlers.Add(OnNewFrameCam3);
            camEventHandlers.Add(OnNewFrameCam4);
        }
    }
}