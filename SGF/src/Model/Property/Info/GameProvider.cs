using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#US
    /// </summary>
    [Label("US")]
    [Description("Name of the user (or program), who entered the game")]
    public class GameProvider : ASimpleTextValue<GameProvider>
    {
        /// <summary>Initialize the property</summary>
        public GameProvider(SimpleText value) : base(value) {}
    }
}
