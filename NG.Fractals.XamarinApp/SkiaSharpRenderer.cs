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

        private SKPaint m_curLinePaint = null;
        private SKPaint m_curFillPaint = null;

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
        public void ClearColour(string hex)
        {
            Xamarin.Forms.Color formsColour = Xamarin.Forms.Color.FromHex(hex);
            m_canvas.DrawColor(formsColour.ToSKColor());
        }

        /// <summary>Sets the colour to render with.</summary>
        public void SetColour(string hex)
        {
            Xamarin.Forms.Color formsColour = Xamarin.Forms.Color.FromHex(hex);
            m_curLinePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = formsColour.ToSKColor(),
                StrokeWidth = 5
            };
            m_curFillPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = formsColour.ToSKColor()
            };
        }

        /// <summary>Draws a line on the target.</summary>
        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            m_canvas.DrawLine((float)x1, (float)y1, (float)x2, (float)y2, m_curLinePaint);
        }

        /// <summary>Draws a circle on the target.</summary>
        public void DrawCircle(double x, double y, double r)
        {
            m_canvas.DrawCircle((float)x, (float)y, (float)r, m_curLinePaint);
        }

        /// <summary>Draws a filled triangle on the target.</summary>
        public void FillTri(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            SKPath path = new SKPath();
            path.MoveTo((float)x1, (float)y1);
            path.LineTo((float)x2, (float)y2);
            path.LineTo((float)x3, (float)y3);
            path.LineTo((float)x1, (float)y1);
            m_canvas.DrawPath(path, m_curFillPaint);
        }

        /// <summary>Draws a filled rectangle on the target.</summary>
        public void FillRect(double left, double top, double width, double height)
        {
            m_canvas.DrawRect((float)left, (float)top, (float)width, (float)height, m_curFillPaint);
        }

        /// <summary>Creates a bitmap for the underlying platform with the canvas size.</summary>
        public IFractalRendererBitmap CreateCanvasBitmap()
        {
            return new SkiaSharpBitmap((int)m_info.Width, (int)m_info.Height);
        }

        /// <summary>Creates a bitmap for the underlying platform.</summary>
        public void DrawBitmap(IFractalRendererBitmap bitmap)
        {
            SkiaSharpBitmap castBitmap = bitmap as SkiaSharpBitmap;
            if (castBitmap != null)
            {
                m_canvas.DrawBitmap(castBitmap.Bitmap, 0.0f, 0.0f);
            }
        }

        #endregion
    }
}
