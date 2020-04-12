using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#GM
    /// </summary>
    [Label("GM")]
    [Description("Type of game, which is stored in the current gametree")]
    public class GameType : AIntegerValue<GameType>
    {
        /// <summary>Initialize the property</summary>
        public GameType(int value) : base(value) {}
    }
}
