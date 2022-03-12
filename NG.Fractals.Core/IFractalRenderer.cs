using System;

namespace NG.Fractals
{
    public interface IFractalRenderer
    {
        /// <summary>The width of what is being rendered to.</summary>
        double Width { get; }
        /// <summary>The width of what is being rendered to.</summary>
        double Height { get; }

        /// <summary>Sets the colour to render with.</summary>
        void SetColour(string hex);

        /// <summary>Draws a line on the target.</summary>
        void DrawLine(double x1, double y1, double x2, double y2);
        /// <summary>Draws a circle on the target.</summary>
        void DrawCircle(double x, double y, double r);
    }
}
