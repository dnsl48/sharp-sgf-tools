using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a move (position on the board) and a colour.
    /// The colour should be assigned by the class attributes <see cref="ColourAttribute" />
    /// </summary>
    public class ColouredMove<T>: APositionValue<T>, IColour
    where T: ColouredMove<T>
    {
        /// <summary>
        /// Initialize with the values
        /// </summary>
        /// <param name="x">X axis of the position</param>
        /// <param name="y">Y axis of the position</param>
        public ColouredMove(byte x, byte y) : base(x, y) {}

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
