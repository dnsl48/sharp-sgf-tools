using System.Collections.Generic;
using dnsl48.SGF.Attributes;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#LN
    /// </summary>
    [Label("LN")]
    [Description("A simple line form one point to the other")]
    public class LineMarker: AVectorSet<LineMarker>
    {
        /// <summary>Initialize the property</summary>
        public LineMarker(IEnumerable<(Point a, Point b)> vectors): base(vectors) {}
    }
}
