using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#BR
    /// </summary>
    [Label("BR")]
    [Description("Rank of the black player")]
    [Colour(Colour.Black)]
    public class BlackPlayerRank : AColouredSimpleTextValue<BlackPlayerRank>
    {
        /// <summary>Initialize the property</summary>
        public BlackPlayerRank(SimpleText value) : base(value) {}
    }
}
