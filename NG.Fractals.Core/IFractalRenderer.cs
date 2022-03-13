using System;

namespace NG.Fractals
{
    public interface IFractalRenderer
    {
        /// <summary>The width of what is being rendered to.</summary>
        double Width { get; }
        /// <summary>The height of what is being rendered to.</summary>
        double Height { get; }

        /// <summary>Sets the colour to render with.</summary>
        void ClearColour(string hex);
        /// <summary>Sets the colour to render with.</summary>
        void SetColour(string hex);

        /// <summary>Draws a line on the target.</summary>
        void DrawLine(double x1, double y1, double x2, double y2);
        /// <summary>Draws a circle on the target.</summary>
        void DrawCircle(double x, double y, double r);

        /// <summary>Draws a filled triangle on the target.</summary>
        void FillTri(double x1, double y1, double x2, double y2, double x3, double y3);
        /// <summary>Draws a filled rectangle on the target.</summary>
        void FillRect(double left, double top, double width, double height);

        /// <summary>Creates a bitmap for the underlying platform with the canvas size.</summary>
        IFractalRendererBitmap CreateCanvasBitmap();
        /// <summary>Creates a bitmap for the underlying platform.</summary>
        void DrawBitmap(IFractalRendererBitmap bitmap);
    }

    public interface IFractalRendererBitmap
    {
        /// <summary>The width.</summary>
        int Width { get; }
        /// <summary>The height.</summary>
        int Height { get; }

        /// <summary>Sets a pixel.</summary>
        void SetPixel(int x, int y, byte red, byte green, byte blue);
    }
}
