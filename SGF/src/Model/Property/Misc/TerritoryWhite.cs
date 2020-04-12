using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#TW
    /// </summary>
    [Label("TW")]
    [Description("Specifies the white territory or area")]
    [Colour(Colour.White)]
    public class TerritoryWhite: AColouredPositionSet<TerritoryWhite>
    {
        /// <summary>Initialize the property</summary>
        public TerritoryWhite(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
