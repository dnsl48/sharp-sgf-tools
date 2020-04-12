using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Abstract model of a SimpleText value with info about its colour.
    /// The colour should be assigned by the class attributes <see cref="ColourAttribute" />.
    /// SimpleText defined by SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#simpletext
    /// </summary>
    public abstract class AColouredSimpleTextValue<T>: ASimpleTextValue<T>, IColour
    where T: AColouredSimpleTextValue<T>
    {
        /// <summary>
        /// Initialize with SimpleText value
        /// </summary>
        /// <param name="value">The value to initialize with</param>
        public AColouredSimpleTextValue(SimpleText value): base(value) {}

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
