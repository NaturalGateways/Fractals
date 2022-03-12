using System;

namespace NG.Fractals
{
    public static class Trig
    {
        public static Vector RotateVectorRes(Vector vec, double sine, double cosine)
        {
            double x2x = cosine * vec.x;
            double y2x = sine * vec.y;
            double x2y = sine * vec.x;
            double y2y = cosine * vec.y;
            return new Vector(x2x - y2x, x2y + y2y);
        }
    }
}
