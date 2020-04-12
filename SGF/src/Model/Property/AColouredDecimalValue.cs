using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Abstract model of a decimal value with info about its colour.
    /// The bolour should be assigned by the class attributes <see cref="ColourAttribute" />
    /// </summary>
    public abstract class AColouredDecimalValue<T>: ADecimalValue<T>, IColour
    where T: AColouredDecimalValue<T>
    {
        /// <summary>
        /// Initialize with decimal value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public AColouredDecimalValue(decimal value): base(value) {}

        /// <summary>
        /// Returns the colour
        /// <seealso cref="ColourAttribute" />
        /// </summary>
        /// <returns>The colour</returns>
        public Colour GetColour()
        {
            var attr = (ColourAttribute) System.Attribute.GetCustomAttribute(typeof(T), typeof(ColourAttribute));
            return attr.GetValue();
        }
    }
}
