using System;
using System.Collections.Generic;

namespace NG.Fractals
{
    public class HeighwayDragonFractal : IFractal
    {
        #region IFractal implementation

        /// <summary>Getter for the fractal name.</summary>
        public string FractalName { get { return "Heighway Dragon"; } }

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
                },
                {
                    "EvenOdd",
                    new FractalProperty
                    {
                        DisplayName = "Even/Odd",
                        PropertyType = FractalPropertyType.Boolean,
                        BoolValue = true
                    }
                }
            };
        }

        /// <summary>Draw the fractal using a renderer.</summary>
        public void Draw(IFractalRenderer renderer, Dictionary<string, IFractalProperty> properties)
        {
            // Get size
            double canvasWidth = renderer.Width;
            double canvasHeight = renderer.Height;

            // Work out start points
            double x1 = 0.0;
            double x2 = 0.0;
            double y = 0.0;
            // Depending on type
            bool evenOdd = properties["EvenOdd"].BoolValue ?? true;
            if (evenOdd)
            {
                x1 = 0.25 * canvasWidth;
                x2 = 0.85 * canvasWidth;
                y = 0.6 * canvasHeight;
            }
            else
            {
                x1 = 0.3 * canvasWidth;
                x2 = 0.7 * canvasWidth;
                y = 0.6 * canvasHeight;
            }

            // Render
            renderer.SetColour("#f00");
            int iteration = properties["Iter"].IntValue ?? 0;
            RenderEdge(renderer, iteration, evenOdd, false, x1, y, x2, y);
        }

        #endregion

        #region Rendering

        /// <summary>Renders an edge of the snowflake.</summary>
        private static void RenderEdge(IFractalRenderer renderer, int iteration, bool evenOdd, bool even, double x1, double y1, double x2, double y2)
        {
            if (iteration == 0)
            {
                renderer.DrawLine(x1, y1, x2, y2);
            }
            else
            {
                Vector edgeVec = new Vector(x2 - x1, y2 - y1);
                Vector paraOffset = new Vector(edgeVec.x / 2, edgeVec.y / 2);
                Vector perpOffset = Trig.RotateVectorRes(paraOffset, (evenOdd && even) ? 1 : -1, 0);
                double midX = x1 + paraOffset.x + perpOffset.x;
                double midY = y1 + paraOffset.y + perpOffset.y;
                --iteration;
                RenderEdge(renderer, iteration, evenOdd, false, x1, y1, midX, midY);
                RenderEdge(renderer, iteration, evenOdd, true, midX, midY, x2, y2);
            }
        }

        #endregion
    }
}
