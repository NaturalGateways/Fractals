using System;
using System.Collections.Generic;

using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace NG.Fractals.XamarinApp
{
    public partial class FractalPage : ContentPage
    {
        private IFractal m_fractal = null;

        private Dictionary<string, IFractalProperty> m_properties = null;

        public FractalPage(IFractal fractal)
        {
            InitializeComponent();

            m_fractal = fractal;
            m_properties = fractal.CreateProperties();
            this.Title = fractal.FractalName;

            foreach (IFractalProperty property in m_properties.Values)
            {
                switch (property.PropertyType)
                {
                    case FractalPropertyType.Integer:
                        this.PropertyStack.Children.Add(new PropertyEditViews.IntegerPropertyEditView(property, RefreshCanvas));
                        break;
                    case FractalPropertyType.Boolean:
                        this.PropertyStack.Children.Add(new PropertyEditViews.BooleanPropertyEditView(property, RefreshCanvas));
                        break;
                    default:
                        this.PropertyStack.Children.Add(new Grid { BackgroundColor = Color.Red, HeightRequest = 50 });
                        break;
                }
            }
        }

        void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            m_fractal.Draw(new SkiaSharpRenderer(args), m_properties);
        }

        private void RefreshCanvas()
        {
            this.SkiaCanvas.InvalidateSurface();
        }
    }
}
