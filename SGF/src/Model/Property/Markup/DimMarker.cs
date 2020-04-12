using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#DD
    /// </summary>
    [Label("DD")]
    [Description("Dim (grey out) the given points")]
    public class DimMarker: APositionSet<DimMarker>
    {
        /// <summary>Initialize the property</summary>
        public DimMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
