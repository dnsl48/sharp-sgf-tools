using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#MA
    /// </summary>
    [Label("MA")]
    [Description("Marks the given points with an 'X'")]
    public class XMarker: APositionSet<XMarker>
    {
        /// <summary>Initialize the property</summary>
        public XMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
