using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#PB
    /// </summary>
    [Label("PB")]
    [Description("Name of the black player")]
    [Colour(Colour.Black)]
    public class BlackPlayerName: AColouredSimpleTextValue<BlackPlayerName>
    {
        /// <summary>Initialize the property</summary>
        public BlackPlayerName(Types.SimpleText name) : base(name) {}
    }
}
