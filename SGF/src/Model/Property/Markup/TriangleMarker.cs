using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#TR
    /// </summary>
    [Label("TR")]
    [Description("Marks the given points with a triangle")]
    public class TriangleMarker: APositionSet<TriangleMarker>
    {
        /// <summary>Initialize the property</summary>
        public TriangleMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
