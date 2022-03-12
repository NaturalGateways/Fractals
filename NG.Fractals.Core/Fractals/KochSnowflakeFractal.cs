using System;
using System.Collections.Generic;

namespace NG.Fractals
{
    public class KochSnowflakeFractal : IFractal
    {
        #region IFractal implementation

        /// <summary>Getter for the fractal name.</summary>
        public string FractalName { get { return "Koch Snowflake"; } }

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
                    "Circle",
                    new FractalProperty
                    {
                        DisplayName = "Circumcircle",
                        PropertyType = FractalPropertyType.Boolean,
                        BoolValue = false
                    }
                },
                {
                    "Invert",
                    new FractalProperty
                    {
                        DisplayName = "Invert",
                        PropertyType = FractalPropertyType.Boolean,
                        BoolValue = false
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
            double fractalSize = 0.95 * Math.Min(canvasWidth, canvasHeight);
            double fractalMidX = canvasWidth / 2;
            double fractalMidY = canvasHeight / 2;
            double fractalRadius = fractalSize / 2;

            // Calculate triangle
            double sine120Deg = Math.Sin((2 * Math.PI) / 3);
            double cosine120Deg = Math.Cos((2 * Math.PI) / 3);
            Vector p1 = new Vector(0.0, -fractalRadius);
            Vector p2 = Trig.RotateVectorRes(p1, sine120Deg, cosine120Deg);
            Vector p3 = Trig.RotateVectorRes(p2, sine120Deg, cosine120Deg);

            // Draw triangle
            renderer.SetColour("#f00");
            int iteration = properties["Iter"].IntValue ?? 0;
            bool invert = properties["Invert"].BoolValue ?? false;
            RenderEdge(renderer, iteration, invert, fractalMidX + p1.x, fractalMidY + p1.y, fractalMidX + p2.x, fractalMidY + p2.y);
            RenderEdge(renderer, iteration, invert, fractalMidX + p2.x, fractalMidY + p2.y, fractalMidX + p3.x, fractalMidY + p3.y);
            RenderEdge(renderer, iteration, invert, fractalMidX + p3.x, fractalMidY + p3.y, fractalMidX + p1.x, fractalMidY + p1.y);

            // Draw circumcircle
            if (properties["Circle"].BoolValue ?? false)
            {
                renderer.SetColour("#00f");
                renderer.DrawCircle(fractalMidX, fractalMidY, fractalRadius);
            }
        }

        #endregion

        #region Rendering

        /// <summary>Renders an edge of the snowflake.</summary>
        private static void RenderEdge(IFractalRenderer renderer, int iteration, bool invert, double x1, double y1, double x2, double y2)
        {
            if (iteration == 0)
            {
                renderer.DrawLine(x1, y1, x2, y2);
            }
            else
            {
                // Calc smaller vectors
                const double SIN60DEG = 0.866025403784439;
                const double COS60DEG = 0.5;
                double sine60Deg = invert ? SIN60DEG : -SIN60DEG;
                Vector edgeVec = new Vector(x2 - x1, y2 - y1);
                Vector thirdVec = new Vector(edgeVec.x / 3, edgeVec.y / 3);
                Vector upVec = Trig.RotateVectorRes(thirdVec, sine60Deg, COS60DEG);
                // Draw iteration
                --iteration;
                RenderEdge(renderer, iteration, invert, x1, y1, x1 + thirdVec.x, y1 + thirdVec.y);
                RenderEdge(renderer, iteration, invert, x1 + thirdVec.x, y1 + thirdVec.y, x1 + thirdVec.x + upVec.x, y1 + thirdVec.y + upVec.y);
                RenderEdge(renderer, iteration, invert, x1 + thirdVec.x + upVec.x, y1 + thirdVec.y + upVec.y, x2 - thirdVec.x, y2 - thirdVec.y);
                RenderEdge(renderer, iteration, invert, x2 - thirdVec.x, y2 - thirdVec.y, x2, y2);
            }
        }

        #endregion
    }
}
