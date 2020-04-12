using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#PW
    /// </summary>
    [Label("PW")]
    [Description("Name of the white player")]
    [Colour(Colour.White)]
    public class WhitePlayerName: AColouredSimpleTextValue<WhitePlayerName>
    {
        /// <summary>Initialize the property</summary>
        public WhitePlayerName(Types.SimpleText name) : base(name) {}
    }
}
