using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Setup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AE
    /// </summary>
    [Label("AE")]
    [Description("Clear the given points on the board")]
    public class ClearPositions: APositionSet<ClearPositions>
    {
        /// <summary>Initialize the property</summary>
        public ClearPositions(IEnumerable<(byte x, byte y)> positions): base(positions) {}
    }
}
