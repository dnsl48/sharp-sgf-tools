using System.Collections.Generic;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing only a decimal value
    /// </summary>
    public abstract class AIntegerValue<T>: AProperty, IIntegerValue
    where T: AIntegerValue<T>
    {
        private int _value;

        /// <summary>
        /// Initialize with the value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public AIntegerValue(int value) => _value = value;

        /// <inheritdoc />
        public int GetValue()
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

            var ctor = typeof(T).GetConstructor(new[] { typeof(int) });
            if (ctor == null)
                return null;

            try {
                var val = int.Parse(values[0].Trim());
                return (IProperty) ctor.Invoke(new object[] { val });
            } catch (System.Exception) {
                return null;
            }
        }
    }
}
