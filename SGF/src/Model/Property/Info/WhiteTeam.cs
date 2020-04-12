using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#WT
    /// </summary>
    [Label("WT")]
    [Description("Name of the white team")]
    [Colour(Colour.White)]
    public class WhiteTeam : AColouredSimpleTextValue<WhiteTeam>
    {
        /// <summary>Initialize the property</summary>
        public WhiteTeam(SimpleText value) : base(value) {}
    }
}
