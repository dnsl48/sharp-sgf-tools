using System.Collections.Generic;
using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#VW
    /// </summary>
    [Label("VW")]
    [Description("View only part of the board")]
    public class BoardVisiblePart : APositionSet<BoardVisiblePart>
    {
        /// <summary>Initialize the property</summary>
        public BoardVisiblePart(IEnumerable<(byte x, byte y)> positions) : base(positions) { }
    }
}
