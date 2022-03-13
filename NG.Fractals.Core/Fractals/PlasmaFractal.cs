using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.Fractals
{
    public class PlasmaFractal : IFractal
    {
        #region Constants

        private const double RANDOMNESS = 0.05;

        #endregion

        #region IFractal implementation

        /// <summary>Getter for the fractal name.</summary>
        public string FractalName { get { return "Plasma"; } }

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
                        IntValue = 10
                    }
                }
            };
        }

        /// <summary>Draw the fractal using a renderer.</summary>
        public void Draw(IFractalRenderer renderer, Dictionary<string, IFractalProperty> properties)
        {
            int iteration = properties["Iter"].IntValue ?? 0;
            double[][] pattern = GeneratePattern(iteration);
            RenderPattern(renderer, pattern);
        }

        #endregion

        #region Pattern generation

        /// <summary>Geneerates a pattern to render.</summary>
        private double[][] GeneratePattern(int iteration)
        {
            Random rnd = new Random();

            // Generate array
            int twoRootIter = 1;
            for (int power = 0; power != iteration; ++power)
                twoRootIter *= 2;
            int arraySize = twoRootIter + 1;
            double[][] pattern = new double[arraySize][];
            for (int rowIndex = 0; rowIndex != arraySize; ++rowIndex)
            {
                pattern[rowIndex] = new double[arraySize];
            }

            // Seed array
            pattern[0][0] = 0.5;
            pattern[twoRootIter][0] = 0.5;
            pattern[0][twoRootIter] = 0.5;
            pattern[twoRootIter][twoRootIter] = 0.5;

            // Populate
            for (int rectSize = twoRootIter; 1 < rectSize; rectSize = rectSize / 2)
            {
                int midSize = rectSize / 2;
                // Diamond iteration
                for (int rowIndex = 0; rowIndex != twoRootIter; rowIndex += rectSize)
                {
                    for (int colIndex = 0; colIndex != twoRootIter; colIndex += rectSize)
                    {
                        double value1 = pattern[rowIndex][colIndex];
                        double value2 = pattern[rowIndex + rectSize][colIndex];
                        double value3 = pattern[rowIndex][colIndex + rectSize];
                        double value4 = pattern[rowIndex + rectSize][colIndex + rectSize];
                        double newValue = ((value1 + value2 + value3 + value4) / 4) + (2 * RANDOMNESS * rnd.NextDouble() - RANDOMNESS);
                        pattern[rowIndex + midSize][colIndex + midSize] = Math.Max(0.0, Math.Min(1.0, newValue));
                    }
                }
                // Row iterations
                for (int rowIndex = 0; rowIndex <= twoRootIter; rowIndex += rectSize)
                {
                    for (int colIndex = 0; colIndex != twoRootIter; colIndex += rectSize)
                    {
                        double sum = pattern[rowIndex][colIndex] + pattern[rowIndex][colIndex + rectSize];
                        double divider = 2;
                        if (rowIndex != 0)
                        {
                            sum += pattern[rowIndex - midSize][colIndex + midSize];
                            ++divider;
                        }
                        if (rowIndex != twoRootIter)
                        {
                            sum += pattern[rowIndex + midSize][colIndex + midSize];
                            ++divider;
                        }
                        double newValue = (sum / divider) + (2 * RANDOMNESS * rnd.NextDouble() - RANDOMNESS);
                        pattern[rowIndex][colIndex + midSize] = Math.Max(0.0, Math.Min(1.0, newValue));
                    }
                }
                // Column iterations
                for (int rowIndex = 0; rowIndex != twoRootIter; rowIndex += rectSize)
                {
                    for (int colIndex = 0; colIndex <= twoRootIter; colIndex += rectSize)
                    {
                        double sum = pattern[rowIndex][colIndex] + pattern[rowIndex + rectSize][colIndex];
                        double divider = 2;
                        if (colIndex != 0)
                        {
                            sum += pattern[rowIndex + midSize][colIndex - midSize];
                            ++divider;
                        }
                        if (colIndex != twoRootIter)
                        {
                            sum += pattern[rowIndex + midSize][colIndex + midSize];
                            ++divider;
                        }
                        double newValue = (sum / divider) + (2 * RANDOMNESS * rnd.NextDouble() - RANDOMNESS);
                        pattern[rowIndex + midSize][colIndex] = Math.Max(0.0, Math.Min(1.0, newValue));
                    }
                }
            }

            // Return
            return pattern;
        }

        #endregion

        #region Rendering

        /// <summary>Geneerates a pattern to render.</summary>
        private void RenderPattern(IFractalRenderer renderer, double[][] pattern)
        {
            // Create bitmap for pixel editing
            IFractalRendererBitmap bitmap = renderer.CreateCanvasBitmap();

            // Draw the pattern
            int columnNum = pattern.First().Length - 1;
            int rowNum = pattern.Length - 1;
            int lastColumnIndex = columnNum - 1;
            int lastRowIndex = rowNum - 1;
            int pixelsPerColumn = bitmap.Width / columnNum;
            int pixelsPerRow = bitmap.Height / rowNum;
            for (int rowIndex = 0; rowIndex != rowNum; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex != columnNum; ++columnIndex)
                {
                    int left = columnIndex * pixelsPerColumn;
                    int top = rowIndex * pixelsPerRow;
                    double topLeftValue = pattern[rowIndex][columnIndex];
                    double topRightValue = pattern[rowIndex][columnIndex + 1];
                    double bottomLeftValue = pattern[rowIndex + 1][columnIndex];
                    double bottomRightValue = pattern[rowIndex + 1][columnIndex + 1];
                    if (columnIndex == lastColumnIndex)
                        if (rowIndex == lastRowIndex)
                            RenderSquare(bitmap, left, bitmap.Width - 1, top, bitmap.Height - 1, topLeftValue, topRightValue, bottomLeftValue, bottomRightValue, true, true);
                        else
                            RenderSquare(bitmap, left, bitmap.Width - 1, top, top + pixelsPerRow, topLeftValue, topRightValue, bottomLeftValue, bottomRightValue, true, false);
                    else
                        if (rowIndex == lastRowIndex)
                            RenderSquare(bitmap, left, left + pixelsPerColumn, top, bitmap.Height - 1, topLeftValue, topRightValue, bottomLeftValue, bottomRightValue, false, true);
                        else
                            RenderSquare(bitmap, left, left + pixelsPerColumn, top, top + pixelsPerRow, topLeftValue, topRightValue, bottomLeftValue, bottomRightValue, false, false);
                }
            }

            // Render
            renderer.DrawBitmap(bitmap);
        }

        /// <summary>Geneerates a pattern to render.</summary>
        private void RenderSquare(IFractalRendererBitmap bitmap, int left, int right, int top, int bottom, double topLeftValue, double topRightValue, double bottomLeftValue, double bottomRightValue, bool incRight, bool incBottom)
        {
            for (int yPixel = top; yPixel != bottom; ++yPixel)
            {
                double yFrac = (((float)yPixel) - top) / (((float)bottom) - top);
                double leftValue = (1.0 - yFrac) * topLeftValue + yFrac * bottomLeftValue;
                double rightValue = (1.0 - yFrac) * topRightValue + yFrac * bottomRightValue;
                for (int xPixel = left; xPixel != right; ++xPixel)
                {
                    double xFrac = (((float)xPixel) - left) / (((float)right) - left);
                    double valueDouble = (1.0 - xFrac) * leftValue + xFrac * rightValue;
                    byte valueByte = (byte)Math.Min(255, (int)(valueDouble * 255.0));
                    bitmap.SetPixel(xPixel, yPixel, valueByte, valueByte, valueByte);
                }
            }
            if (incRight)
            {
                for (int yPixel = top; yPixel != bottom; ++yPixel)
                {
                    double yFrac = (((float)yPixel) - top) / (((float)bottom) - top);
                    double valueDouble = (1.0 - yFrac) * topRightValue + yFrac * bottomRightValue;
                    byte valueByte = (byte)Math.Min(255, (int)(valueDouble * 255.0));
                    bitmap.SetPixel(right, yPixel, valueByte, valueByte, valueByte);
                }
            }
            if (incBottom)
            {
                for (int xPixel = left; xPixel != right; ++xPixel)
                {
                    double xFrac = (((float)xPixel) - left) / (((float)right) - left);
                    double valueDouble = (1.0 - xFrac) * bottomLeftValue + xFrac * bottomRightValue;
                    byte valueByte = (byte)Math.Min(255, (int)(valueDouble * 255.0));
                    bitmap.SetPixel(xPixel, bottom, valueByte, valueByte, valueByte);
                }
            }
            if (incRight && incBottom)
            {
                byte valueByte = (byte)Math.Min(255, (int)(bottomRightValue * 255.0));
                bitmap.SetPixel(right, bottom, valueByte, valueByte, valueByte);
            }
        }

        #endregion
    }
}
