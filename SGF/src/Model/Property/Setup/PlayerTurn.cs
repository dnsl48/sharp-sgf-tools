using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Setup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#PL
    /// </summary>
    [Label("PL")]
    [Description("Whose turn it is to play")]
    public class PlayerTurn: AColourValue<PlayerTurn>
    {
        /// <summary>Initialize the property</summary>
        public PlayerTurn(Colour value): base(value) {}
    }
}
