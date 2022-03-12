using System;

namespace NG.Fractals.XamarinApp
{
    public class FractalListItemViewModel
    {
        public IFractal Fractal { get; private set; }

        public FractalListItemViewModel(IFractal fractal)
        {
            this.Fractal = fractal;
        }

        public string FractalName { get { return this.Fractal.FractalName; } }
    }
}
