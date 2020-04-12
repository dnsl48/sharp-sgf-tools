using System.Collections.Generic;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a Text value.
    /// Text defined by SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#text
    /// </summary>
    public abstract class ATextValue<T> : AProperty, ITextValue
    where T : ATextValue<T>
    {
        private Text _value;

        /// <summary>
        /// Initialize with the value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public ATextValue(Text value) => _value = value;

        /// <summary>Returns the property value</summary>
        /// <returns>The Text value</returns>
        public Text GetValue()
        {
            return _value;
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[{GetValue()}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count != 1)
                return null;

            var ctor = typeof(T).GetConstructor(new[] { typeof(Text) });
            if (ctor == null)
                return null;

            var val = Text.parse(values[0]);
            if (val == null)
                return null;

            return (IProperty) ctor.Invoke(new object[] { val });
        }
    }
}
