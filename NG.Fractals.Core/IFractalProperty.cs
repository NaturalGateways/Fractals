using System;

namespace NG.Fractals
{
    public enum FractalPropertyType
    {
        Integer,
        Boolean
    }

    public interface IFractalProperty
    {
        /// <summary>The display name of the property.</summary>
        string DisplayName { get; }

        /// <summary>The type of the property.</summary>
        FractalPropertyType PropertyType { get; }

        /// <summary>The int value.</summary>
        int? IntValue { get; set; }

        /// <summary>The int value.</summary>
        bool? BoolValue { get; set; }
    }

    public class FractalProperty : IFractalProperty
    {
        /// <summary>The display name of the property.</summary>
        public string DisplayName { get; set; }

        /// <summary>The type of the property.</summary>
        public FractalPropertyType PropertyType { get; set; }

        /// <summary>The int value.</summary>
        public int? IntValue { get; set; }

        /// <summary>The int value.</summary>
        public bool? BoolValue { get; set; }
    }
}
