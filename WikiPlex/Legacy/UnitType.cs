namespace WikiPlex.Legacy
{
    
    
    /// <summary><para>Specifies the unit of measurement.</para></summary>
    public enum UnitType
    {
        /// <summary><para>Measurement is in pixels.</para></summary>
        Pixel = 1,
        /// <summary><para>Measurement is in points. A point represents 1/72 of an inch.</para></summary>
        Point = 2,
        /// <summary><para>Measurement is in picas. A pica represents 12 points.</para></summary>
        Pica = 3,
        /// <summary><para>Measurement is in inches.</para></summary>
        Inch = 4,
        /// <summary><para>Measurement is in millimeters.</para></summary>
        Mm = 5,
        /// <summary><para>Measurement is in centimeters.</para></summary>
        Cm = 6,
        /// <summary><para>Measurement is a percentage relative to the parent element.</para></summary>
        Percentage = 7,
        /// <summary><para>Measurement is relative to the height of the parent element's font.</para></summary>
        Em = 8,
        /// <summary><para>Measurement is relative to the height of the lowercase letter x of the parent element's font.</para></summary>
        Ex = 9,
    }
    
    
}