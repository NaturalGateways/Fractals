using System;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace NG.Fractals.XamarinApp
{
    public class SkiaSharpRenderer : IFractalRenderer
    {
        #region Base

        private SKImageInfo m_info;
        private SKSurface m_surface = null;
        private SKCanvas m_canvas = null;

        private SKPaint m_curPaint = null;

        /// <summary>Constructor.</summary>
        public SkiaSharpRenderer(SKPaintSurfaceEventArgs args)
        {
            // Set vars
            m_info = args.Info;
            m_surface = args.Surface;
            m_canvas = m_surface.Canvas;

            // Clear canvas
            m_canvas.Clear();
        }

        #endregion

        #region IFractalRenderer implementation

        /// <summary>The width of what is being rendered to.</summary>
        public double Width { get { return m_info.Width; } }

        /// <summary>The width of what is being rendered to.</summary>
        public double Height { get { return m_info.Height; } }

        /// <summary>Sets the colour to render with.</summary>
        public void SetColour(string hex)
        {
            Xamarin.Forms.Color formsColour = Xamarin.Forms.Color.FromHex(hex);
            m_curPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = formsColour.ToSKColor(),
                StrokeWidth = 5
            };
        }

        /// <summary>Draws a line on the target.</summary>
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            m_canvas.DrawLine((float)x1, (float)y1, (float)x2, (float)y2, m_curPaint);
        }

        /// <summary>Draws a circle on the target.</summary>
        public void DrawCircle(double x, double y, double r)
        {
            m_canvas.DrawCircle((float)x, (float)y, (float)r, m_curPaint);
        }

        #endregion
    }
}
