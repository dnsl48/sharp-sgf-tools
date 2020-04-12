using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#CR
    /// </summary>
    [Label("CR")]
    [Description("Marks the given points with a circle")]
    public class CircleMarker: APositionSet<CircleMarker>
    {
        /// <summary>Initialize the property</summary>
        public CircleMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
