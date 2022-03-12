using System;

namespace NG.Fractals
{
    public static class FractalFactory
    {
        public static IFractal CreateSingleFractal()
        {
            return new HeighwayDragonFractal();
        }

        public static IFractal[] CreateFractals()
        {
            return new IFractal[]
            {
                new KochSnowflakeFractal(),
                new HeighwayDragonFractal()
            };
        }
    }
}
