using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#GN
    /// </summary>
    [Label("GN")]
    [Description("Name of the game")]
    public class GameName : ASimpleTextValue<GameName>
    {
        /// <summary>Initialize the property</summary>
        public GameName(SimpleText name) : base(name) {}
    }
}
