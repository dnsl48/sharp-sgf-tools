using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Abstract model of an integer value with info about its colour.
    /// The colour should be assigned by the class attributes <see cref="ColourAttribute" />
    /// </summary>
    public abstract class AColouredIntegerValue<T>: AIntegerValue<T>, IColour
    where T: AColouredIntegerValue<T>
    {
        /// <summary>
        /// Initialize with integer value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public AColouredIntegerValue(int value): base(value) {}

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
