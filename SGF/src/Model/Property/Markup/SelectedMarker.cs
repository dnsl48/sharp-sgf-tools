using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#SL
    /// </summary>
    [Label("SL")]
    [Description("Selected points")]
    public class SelectedMarker: APositionSet<SelectedMarker>
    {
        /// <summary>Initialize the property</summary>
        public SelectedMarker(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
