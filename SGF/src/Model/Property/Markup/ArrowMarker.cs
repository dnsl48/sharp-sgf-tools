using System.Collections.Generic;
using dnsl48.SGF.Attributes;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AR
    /// </summary>
    [Label("AR")]
    [Description("Arrow pointing FROM the first point TO the second point")]
    public class ArrowMarker: AVectorSet<ArrowMarker>
    {
        /// <summary>Initialize the property</summary>
        public ArrowMarker(IEnumerable<(Point a, Point b)> vectors): base(vectors) {}
    }
}
