using dnsl48.SGF.Types;
using System.Collections.Generic;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a SimpleText value.
    /// SimpleText defined by SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#simpletext
    /// </summary>
    public abstract class ASimpleTextValue<T> : AProperty, ISimpleTextValue
    where T: ASimpleTextValue<T>
    {
        private SimpleText _value;

        /// <summary>
        /// Initialize with the value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public ASimpleTextValue(SimpleText value) => _value = value;

        /// <inheritdoc />
        public SimpleText GetValue()
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

            var ctor = typeof(T).GetConstructor(new[] { typeof(SimpleText) });
            if (ctor == null)
                return null;

            var val = SimpleText.parse(values[0]);
            if (val == null)
            return null;

            return (IProperty) ctor.Invoke(new object[] { val });
        }
    }
}
