namespace TargetTracker
{
    public struct PointD
    {
        public double X, Y;

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointD(PointD p)
        {
            X = p.X;
            Y = p.Y;
        }
    }
}
