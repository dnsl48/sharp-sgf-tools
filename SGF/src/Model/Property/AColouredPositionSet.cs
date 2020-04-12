using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Abstract model of a position set value with info about its colour.
    /// The colour should be assigned by the class attributes <see cref="ColourAttribute" />
    /// </summary>
    public abstract class AColouredPositionSet<T>: APositionSet<T>, IColour
    where T: AColouredPositionSet<T>
    {
        /// <summary>
        /// Initialize with positions
        /// </summary>
        /// <param name="positions">The values to initialize with</param>
        public AColouredPositionSet(IEnumerable<(byte x, byte y)> positions): base(positions) {}

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
