using System;
using System.Collections.Generic;

namespace NG.Fractals
{
    public class SierpinskiCarpetFractal : IFractal
    {
        #region IFractal implementation

        /// <summary>Getter for the fractal name.</summary>
        public string FractalName { get { return "Sierpinski Carpet"; } }

        /// <summary>Creates a set of properties for the fractal.</summary>
        public Dictionary<string, IFractalProperty> CreateProperties()
        {
            return new Dictionary<string, IFractalProperty>
            {
                {
                    "Iter",
                    new FractalProperty
                    {
                        DisplayName = "Iteration",
                        PropertyType = FractalPropertyType.Integer,
                        IntValue = 0
                    }
                }
            };
        }

        /// <summary>Draw the fractal using a renderer.</summary>
        public void Draw(IFractalRenderer renderer, Dictionary<string, IFractalProperty> properties)
        {
            // Get size
            const double MARGIN = 32.0;
            double canvasWidth = renderer.Width;
            double canvasHeight = renderer.Height;
            double fractalSize = Math.Min(canvasWidth, canvasHeight) - MARGIN;
            double fractalLeft = (canvasWidth - fractalSize) / 2;
            double fractalTop = (canvasHeight - fractalSize) / 2;

            // Clear
            renderer.ClearColour("#ccc");

            // Draw square
            renderer.SetColour("#44F");
            renderer.FillRect(fractalLeft, fractalTop, fractalSize, fractalSize);

            // Draw holes if it has iterations
            int iteration = properties["Iter"].IntValue ?? 0;
            if (0 < iteration)
            {
                double maxIterationSize = 4 * fractalSize;
                double iterationSize = Math.Pow(3, iteration);
                while (maxIterationSize < iterationSize && 2 < iteration)
                {
                    --iteration;
                    iterationSize = iterationSize / 3;
                }

                // Render
                renderer.SetColour("#ccc");
                RenderSquare(renderer, iteration, fractalLeft, fractalTop, fractalSize);
            }
        }

        #endregion

        #region Rendering

        /// <summary>Renders an edge of the snowflake.</summary>
        private static void RenderSquare(IFractalRenderer renderer, int iteration, double left, double top, double size)
        {
            double subSize = size / 3;

            // Draw hole
            renderer.FillRect(left + subSize, top + subSize, subSize, subSize);

            // Recurse
            if (1 < iteration)
            {
                --iteration;
                RenderSquare(renderer, iteration, left, top, subSize);
                RenderSquare(renderer, iteration, left + subSize, top, subSize);
                RenderSquare(renderer, iteration, left + subSize + subSize, top, subSize);
                RenderSquare(renderer, iteration, left, top + subSize, subSize);
                RenderSquare(renderer, iteration, left + subSize + subSize, top + subSize, subSize);
                RenderSquare(renderer, iteration, left, top + subSize + subSize, subSize);
                RenderSquare(renderer, iteration, left + subSize, top + subSize + subSize, subSize);
                RenderSquare(renderer, iteration, left + subSize + subSize, top + subSize + subSize, subSize);
            }
        }

        #endregion
    }
}
