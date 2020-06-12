
namespace WikiPlex.Legacy
{
    
    
    public class UnitConverter 
        : System.ComponentModel.TypeConverter
    {
        /// <internalonly/>
        /// <devdoc>
        ///   Returns a value indicating whether the unit converter can 
        ///   convert from the specified source type.
        /// </devdoc>
        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }


        /// <internalonly/>
        /// <devdoc>
        ///   Returns a value indicating whether the converter can
        ///   convert to the specified destination type.
        /// </devdoc>
        public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType)
        {
            if ((destinationType == typeof(string)) ||
                (destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }


        /// <internalonly/>
        /// <devdoc>
        ///   Performs type conversion from the given value into a Unit.
        /// </devdoc>
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value == null)
                return null;

            string stringValue = value as string;
            if (stringValue != null)
            {
                string textValue = stringValue.Trim();
                if (textValue.Length == 0)
                {
                    return Unit.Empty;
                }

                if (culture != null)
                {
                    return Unit.Parse(textValue, culture);
                }
                else
                {
                    return Unit.Parse(textValue, System.Globalization.CultureInfo.CurrentCulture);
                }
            }
            else
            {
                return base.ConvertFrom(context, culture, value);
            }
        }


        /// <internalonly/>
        /// <devdoc>
        ///   Performs type conversion to the specified destination type
        /// </devdoc>
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value,
            System.Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if ((value == null) || ((Unit) value).IsEmpty)
                    return string.Empty;
                else
                    return ((Unit) value).ToString(culture);
            }
            else if ((destinationType == typeof(System.ComponentModel.Design.Serialization.InstanceDescriptor)) && (value != null))
            {
                Unit u = (Unit) value;
                System.Reflection.MemberInfo member = null;
                object[] args = null;

                if (u.IsEmpty)
                {
                    member = typeof(Unit).GetField("Empty");
                }
                else
                {
                    member = typeof(Unit).GetConstructor(new System.Type[] {typeof(double), typeof(WikiPlex.Legacy.UnitType)});
                    args = new object[] {u.Value, u.Type};
                }

                // Debug.Assert(member != null, "Looks like we're missing Unit.Empty or Unit::ctor(double, UnitType)");
                if (member != null)
                {
                    return new System.ComponentModel.Design.Serialization.InstanceDescriptor(member, args);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}