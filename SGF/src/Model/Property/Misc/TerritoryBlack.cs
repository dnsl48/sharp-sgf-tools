using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#TB
    /// </summary>
    [Label("TB")]
    [Description("Specifies the black territory or area")]
    [Colour(Colour.Black)]
    public class TerritoryBlack: AColouredPositionSet<TerritoryBlack>
    {
        /// <summary>Initialize the property</summary>
        public TerritoryBlack(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
