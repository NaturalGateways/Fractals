using System;
using System.Collections.Generic;

namespace NG.Fractals
{
    public interface IFractal
    {
        /// <summary>Getter for the fractal name.</summary>
        string FractalName { get; }

        /// <summary>Creates a set of properties for the fractal.</summary>
        Dictionary<string, IFractalProperty> CreateProperties();

        /// <summary>Draw the fractal using a renderer.</summary>
        void Draw(IFractalRenderer renderer, Dictionary<string, IFractalProperty> properties);
    }
}
