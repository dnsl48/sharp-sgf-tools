using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#SQ
    /// </summary>
    [Label("SQ")]
    [Description("Marks the given points with a square")]
    public class SquareMarker: APositionSet<SquareMarker>
    {
        /// <summary>Initialize the property</summary>
        public SquareMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
