using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#WR
    /// </summary>
    [Label("WR")]
    [Description("Rank of the white player")]
    [Colour(Colour.White)]
    public class WhitePlayerRank: AColouredSimpleTextValue<WhitePlayerRank>
    {
        /// <summary>Initialize the property</summary>
        public WhitePlayerRank(SimpleText value) : base(value) {}
    }
}
