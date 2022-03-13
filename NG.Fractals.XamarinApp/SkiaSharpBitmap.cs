using System;

using SkiaSharp;

namespace NG.Fractals.XamarinApp
{
    public class SkiaSharpBitmap : IFractalRendererBitmap
    {
        #region Base

        /// <summary>The bitmap.</summary>
        public SKBitmap Bitmap { get; private set; }

        /// <summary>Constructor.</summary>
        public SkiaSharpBitmap(int width, int height)
        {
            this.Bitmap = new SKBitmap(width, height);
        }

        #endregion

        #region IFractalRendererBitmap implementation

        /// <summary>The width.</summary>
        public int Width { get { return this.Bitmap.Width; } }
        /// <summary>The height.</summary>
        public int Height { get { return this.Bitmap.Height; } }

        /// <summary>Sets a pixel.</summary>
        public void SetPixel(int x, int y, byte red, byte green, byte blue)
        {
            this.Bitmap.SetPixel(x, y, new SKColor(red, green, blue));
        }

        #endregion
    }
}
