
namespace WikiPlex.Legacy
{


    [System.ComponentModel.TypeConverterAttribute(typeof(WikiPlex.Legacy.UnitConverter))]
    [System.Serializable]
    public struct Unit
    {
        /// <devdoc>
        ///   Specifies an empty unit.
        /// </devdoc>
        public static readonly Unit Empty = new Unit();

        internal const int MaxValue = 32767;
        internal const int MinValue = -32768;

        private readonly WikiPlex.Legacy.UnitType type;
        private readonly double value;


        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Web.UI.WebControls.Unit'/> structure with the specified 32-bit signed integer as 
        ///    the unit value and <see langword='Pixel'/> as the (default) unit type.</para>
        /// </devdoc>
        public Unit(int value)
        {
            if ((value < MinValue) || (value > MaxValue))
            {
                throw new System.ArgumentOutOfRangeException("value");
            }

            this.value = value;
            this.type = WikiPlex.Legacy.UnitType.Pixel;
        }


        /// <devdoc>
        /// <para> Initializes a new instance of the <see cref='System.Web.UI.WebControls.Unit'/> structure with the 
        ///    specified double-precision
        ///    floating point number as the unit value and <see langword='Pixel'/>
        ///    as the (default) unit type.</para>
        /// </devdoc>
        public Unit(double value)
        {
            if ((value < MinValue) || (value > MaxValue))
            {
                throw new System.ArgumentOutOfRangeException("value");
            }

            this.value = (int) value;
            this.type = WikiPlex.Legacy.UnitType.Pixel;
        }


        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Web.UI.WebControls.Unit'/> structure with the specified 
        ///    double-precision floating point number as the unit value and the specified
        /// <see cref='System.Web.UI.WebControls.UnitType'/> as the unit type.</para>
        /// </devdoc>
        public Unit(double value, WikiPlex.Legacy.UnitType type)
        {
            if ((value < MinValue) || (value > MaxValue))
            {
                throw new System.ArgumentOutOfRangeException("value");
            }

            if (type == WikiPlex.Legacy.UnitType.Pixel)
            {
                this.value = (int) value;
            }
            else
            {
                this.value = value;
            }

            this.type = type;
        }


        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.Web.UI.WebControls.Unit'/> structure with the specified text 
        ///    string that contains the unit value and unit type. If the unit type is not
        ///    specified, the default is <see langword='Pixel'/>
        ///    . </para>
        /// </devdoc>
        public Unit(string value)
            : this(value, System.Globalization.CultureInfo.CurrentCulture, WikiPlex.Legacy.UnitType.Pixel)
        {
        }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public Unit(string value, System.Globalization.CultureInfo culture)
            : this(value, culture, WikiPlex.Legacy.UnitType.Pixel)
        {
        }

        internal Unit(string value, System.Globalization.CultureInfo culture, WikiPlex.Legacy.UnitType defaultType)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.value = 0;
                this.type = (WikiPlex.Legacy.UnitType) 0;
            }
            else
            {
                if (culture == null)
                {
                    culture = System.Globalization.CultureInfo.CurrentCulture;
                }

                // This is invariant because it acts like an enum with a number together. 
                // The enum part is invariant, but the number uses current culture. 
                string trimLcase = value.Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture);
                int len = trimLcase.Length;

                int lastDigit = -1;
                for (int i = 0; i < len; i++)
                {
                    char ch = trimLcase[i];
                    if (((ch < '0') || (ch > '9')) && (ch != '-') && (ch != '.') && (ch != ','))
                        break;
                    lastDigit = i;
                }

                if (lastDigit == -1)
                {
                    throw new System.FormatException("\"" + value + "\" is not digit...");
                }

                if (lastDigit < len - 1)
                {
                    type = (WikiPlex.Legacy.UnitType) GetTypeFromString(trimLcase.Substring(lastDigit + 1).Trim());
                }
                else
                {
                    type = defaultType;
                }

                string numericPart = trimLcase.Substring(0, lastDigit + 1);
                // Cannot use Double.FromString, because we don't use it in the ToString implementation
                try
                {
                    System.ComponentModel.TypeConverter converter = new System.ComponentModel.SingleConverter();
                    this.value = (System.Single) converter.ConvertFromString(null, culture, numericPart);

                    if (type == WikiPlex.Legacy.UnitType.Pixel)
                    {
                        this.value = (int) this.value;
                    }
                }
                catch
                {
                    throw new System.FormatException("\"" + value + "\" UnitParseNumericPart \"" + numericPart + "\" Type: " +
                                                     type.ToString("G"));
                }

                if ((this.value < MinValue) || (this.value > MaxValue))
                {
                    throw new System.ArgumentOutOfRangeException("value");
                }
            }
        }


        /// <devdoc>
        /// <para>Gets a value indicating whether the <see cref='System.Web.UI.WebControls.Unit'/> is empty.</para>
        /// </devdoc>
        public bool IsEmpty
        {
            get { return type == (WikiPlex.Legacy.UnitType) 0; }
        }


        /// <devdoc>
        /// <para>Gets or sets the type of the <see cref='System.Web.UI.WebControls.Unit'/> .</para>
        /// </devdoc>
        public WikiPlex.Legacy.UnitType Type
        {
            get
            {
                if (!IsEmpty)
                {
                    return this.type;
                }
                else
                {
                    return WikiPlex.Legacy.UnitType.Pixel;
                }
            }
        }


        /// <devdoc>
        /// <para>Gets the value of the <see cref='System.Web.UI.WebControls.Unit'/> .</para>
        /// </devdoc>
        public double Value
        {
            get { return this.value; }
        }


        internal static int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public override int GetHashCode()
        {
            return CombineHashCodes(type.GetHashCode(), value.GetHashCode());
        }


        /// <devdoc>
        /// <para>Compares this <see cref='System.Web.UI.WebControls.Unit'/> with the specified object.</para>
        /// </devdoc>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Unit))
            {
                return false;
            }

            Unit u = (Unit) obj;

            // compare internal values to avoid "defaulting" in the case of "Empty"
            //
            if (u.type == type && u.value == value)
            {
                return true;
            }

            return false;
        }


        /// <devdoc>
        ///    <para>Compares two units to find out if they have the same value and type.</para>
        /// </devdoc>
        public static bool operator ==(Unit left, Unit right)
        {
            // compare internal values to avoid "defaulting" in the case of "Empty"
            //
            return (left.type == right.type && left.value == right.value);
        }


        /// <devdoc>
        ///    <para>Compares two units to find out if they have different
        ///       values and/or types.</para>
        /// </devdoc>
        public static bool operator !=(Unit left, Unit right)
        {
            // compare internal values to avoid "defaulting" in the case of "Empty"
            //
            return (left.type != right.type || left.value != right.value);
        }


        /// <devdoc>
        ///  Converts UnitType to persistence string.
        /// </devdoc>
        private static string GetStringFromType(WikiPlex.Legacy.UnitType type)
        {
            switch (type)
            {
                case WikiPlex.Legacy.UnitType.Pixel:
                    return "px";
                case WikiPlex.Legacy.UnitType.Point:
                    return "pt";
                case WikiPlex.Legacy.UnitType.Pica:
                    return "pc";
                case WikiPlex.Legacy.UnitType.Inch:
                    return "in";
                case WikiPlex.Legacy.UnitType.Mm:
                    return "mm";
                case WikiPlex.Legacy.UnitType.Cm:
                    return "cm";
                case WikiPlex.Legacy.UnitType.Percentage:
                    return "%";
                case WikiPlex.Legacy.UnitType.Em:
                    return "em";
                case WikiPlex.Legacy.UnitType.Ex:
                    return "ex";
            }

            return string.Empty;
        }


        /// <devdoc>
        ///  Converts persistence string to UnitType.
        /// </devdoc>
        private static WikiPlex.Legacy.UnitType GetTypeFromString(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Equals("px"))
                {
                    return WikiPlex.Legacy.UnitType.Pixel;
                }
                else if (value.Equals("pt"))
                {
                    return WikiPlex.Legacy.UnitType.Point;
                }
                else if (value.Equals("%"))
                {
                    return WikiPlex.Legacy.UnitType.Percentage;
                }
                else if (value.Equals("pc"))
                {
                    return WikiPlex.Legacy.UnitType.Pica;
                }
                else if (value.Equals("in"))
                {
                    return WikiPlex.Legacy.UnitType.Inch;
                }
                else if (value.Equals("mm"))
                {
                    return WikiPlex.Legacy.UnitType.Mm;
                }
                else if (value.Equals("cm"))
                {
                    return WikiPlex.Legacy.UnitType.Cm;
                }
                else if (value.Equals("em"))
                {
                    return WikiPlex.Legacy.UnitType.Em;
                }
                else if (value.Equals("ex"))
                {
                    return WikiPlex.Legacy.UnitType.Ex;
                }
                else
                {
                    throw new System.ArgumentOutOfRangeException("value");
                }
            }

            return WikiPlex.Legacy.UnitType.Pixel;
        }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public static Unit Parse(string s)
        {
            return new Unit(s, System.Globalization.CultureInfo.CurrentCulture);
        }


        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public static Unit Parse(string s, System.Globalization.CultureInfo culture)
        {
            return new Unit(s, culture);
        }


        /// <devdoc>
        /// <para>Creates a <see cref='System.Web.UI.WebControls.Unit'/> of type <see langword='Percentage'/> from the specified 32-bit signed integer.</para>
        /// </devdoc>
        public static Unit Percentage(double n)
        {
            return new Unit(n, WikiPlex.Legacy.UnitType.Percentage);
        }


        /// <devdoc>
        /// <para>Creates a <see cref='System.Web.UI.WebControls.Unit'/> of type <see langword='Pixel'/> from the specified 32-bit signed integer.</para>
        /// </devdoc>
        public static Unit Pixel(int n)
        {
            return new Unit(n);
        }


        /// <devdoc>
        /// <para>Creates a <see cref='System.Web.UI.WebControls.Unit'/> of type <see langword='Point'/> from the 
        ///    specified 32-bit signed integer.</para>
        /// </devdoc>
        public static Unit Point(int n)
        {
            return new Unit(n, WikiPlex.Legacy.UnitType.Point);
        }


        /// <internalonly/>
        /// <devdoc>
        /// <para>Converts a <see cref='System.Web.UI.WebControls.Unit'/> to a <see cref='System.String' qualify='true'/> .</para>
        /// </devdoc>
        public override string ToString()
        {
            return ToString((System.IFormatProvider) System.Globalization.CultureInfo.CurrentCulture);
        }


        public string ToString(System.Globalization.CultureInfo culture)
        {
            return ToString((System.IFormatProvider) culture);
        }


        public string ToString(System.IFormatProvider formatProvider)
        {
            if (IsEmpty)
                return string.Empty;

            // Double.ToString does not do the right thing, we get extra bits at the end
            string valuePart;
            if (type == WikiPlex.Legacy.UnitType.Pixel)
            {
                valuePart = ((int) value).ToString(formatProvider);
            }
            else
            {
                valuePart = ((float) value).ToString(formatProvider);
            }

            return valuePart + Unit.GetStringFromType(type);
        }


        /// <devdoc>
        /// <para>Implicitly creates a <see cref='System.Web.UI.WebControls.Unit'/> of type <see langword='Pixel'/> from the specified 32-bit unsigned integer.</para>
        /// </devdoc>
        public static implicit operator Unit(int n)
        {
            return Unit.Pixel(n);
        }
    }
}