using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Setup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AW
    /// </summary>
    [Label("AW")]
    [Description("Add white stones to the board")]
    [Colour(Colour.White)]
    public class AddWhitePositions: AColouredPositionSet<AddWhitePositions>
    {
        /// <summary>Initialize the property</summary>
        public AddWhitePositions(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
