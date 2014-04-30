using System.Drawing;

namespace TargetTracker
{
    public class LazerSpot
    {
        public byte patRed, patGreen, patBlue;
        public int squareDev;
        public int minPointsCount;
        /// <summary>
        /// если размер (W && H) меньше указанного - пятно считается точкой
        /// </summary>
        public int maxPointsToConsiderDot;

        public LazerSpot() {}

        public LazerSpot(Color ptColor, int squareDev, int minPointsCount, int maxPointsToConsiderDot)
        {
            this.squareDev = squareDev;
            this.minPointsCount = minPointsCount;
            this.maxPointsToConsiderDot = maxPointsToConsiderDot;
            patRed = ptColor.R;
            patGreen = ptColor.G;
            patBlue = ptColor.B;
        }
    }
}
