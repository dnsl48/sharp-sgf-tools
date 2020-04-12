using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#TM
    /// </summary>
    [Label("TM")]
    [Description("Time limits of the game (in seconds)")]
    public class GameTimeLimit: ADecimalValue<GameTimeLimit>
    {
        /// <summary>Initialize the property</summary>
        public GameTimeLimit(decimal value) : base(value) {}
    }
}
