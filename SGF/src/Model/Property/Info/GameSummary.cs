using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#GC
    /// </summary>
    [Label("GC")]
    [Description("Extra information about the game. Background information or summary of the game.")]
    public class GameSummary : ATextValue<GameSummary>
    {
        /// <summary>Initialize the property</summary>
        public GameSummary(Text value) : base(value) {}
    }
}
