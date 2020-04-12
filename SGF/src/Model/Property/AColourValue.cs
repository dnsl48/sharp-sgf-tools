using System.Collections.Generic;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing only a colour value
    /// </summary>
    public abstract class AColourValue<T>: AProperty, IColour
    where T: AColourValue<T>
    {
        private Colour _value;

        /// <summary>
        /// Initialize with the colour
        /// </summary>
        /// <param name="value">Colour value for the property</param>
        public AColourValue(Colour value) => _value = value;

        /// <summary>
        /// Returns the colour value
        /// </summary>
        public Colour GetColour()
        {
            return _value;
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[{GetColour().GetLabel()}]";
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

            var ctor = typeof(T).GetConstructor(new[] { typeof(Colour) });
            if (ctor == null)
                return null;

            try {
                Colour val;

                switch (values[0].Trim().ToUpper())
                {
                    case "B":
                        val = Colour.Black;
                        break;

                    case "W":
                        val = Colour.White;
                        break;

                    default:
                        return null;
                }

                return (IProperty) ctor.Invoke(new object[] { val });

            } catch (System.Exception) {
                return null;
            }
        }
    }
}
