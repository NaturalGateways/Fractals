using System;
using System.Collections.Generic;

namespace NG.Fractals
{
    public class SierpinskiTriangleFractal : IFractal
    {
        #region IFractal implementation

        /// <summary>Getter for the fractal name.</summary>
        public string FractalName { get { return "Sierpinski Triangle"; } }

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
            // Calculate aspect ratio
            double aspectWOverH = 1.154700538379252;

            // Get size
            const double MARGIN = 32.0;
            double canvasWidth = renderer.Width;
            double canvasHeight = renderer.Height;
            double fractalWidth = Math.Min(canvasWidth, canvasHeight * aspectWOverH) - MARGIN;
            double fractalHeight = fractalWidth / aspectWOverH;
            double fractalLeft = (canvasWidth - fractalWidth) / 2;
            double fractalTop = (canvasHeight - fractalHeight) / 2;

            // Clear
            renderer.ClearColour("#ccc");

            // Draw triangle
            renderer.SetColour("#44F");
            int iteration = properties["Iter"].IntValue ?? 0;
            RenderTriangle(renderer, iteration, fractalLeft, fractalLeft + fractalWidth, fractalTop, fractalTop + fractalHeight);
        }

        #endregion

        #region Rendering

        /// <summary>Renders an edge of the snowflake.</summary>
        private static void RenderTriangle(IFractalRenderer renderer, int iteration, double left, double right, double top, double bottom)
        {
            if (iteration == 0)
            {
                renderer.FillTri(left, bottom, (left + right) / 2, top, right, bottom);
            }
            else
            {
                --iteration;
                double xMid = (left + right) / 2;
                double yMid = (top + bottom) / 2;
                double xQuad1 = (left + xMid) / 2;
                double xQuad3 = (xMid + right) / 2;
                RenderTriangle(renderer, iteration, left, xMid, yMid, bottom);
                RenderTriangle(renderer, iteration, xQuad1, xQuad3, top, yMid);
                RenderTriangle(renderer, iteration, xMid, right, yMid, bottom);
            }
        }

        #endregion
    }
}
