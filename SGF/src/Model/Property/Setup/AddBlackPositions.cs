using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Setup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AB
    /// </summary>
    [Label("AB")]
    [Description("Add black stones to the board")]
    [Colour(Colour.Black)]
    public class AddBlackPositions: AColouredPositionSet<AddBlackPositions>
    {
        /// <summary>Initialize the property</summary>
        public AddBlackPositions(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
