using System;

namespace NG.Fractals
{
    public static class FractalFactory
    {
        public static IFractal CreateSingleFractal()
        {
            return new PlasmaFractal();
        }

        public static IFractal[] CreateFractals()
        {
            return new IFractal[]
            {
                new KochSnowflakeFractal(),
                new HeighwayDragonFractal(),
                new SierpinskiTriangleFractal(),
                new SierpinskiCarpetFractal(),
                new PlasmaFractal()
            };
        }
    }
}
