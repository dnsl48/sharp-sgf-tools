using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#V
    /// </summary>
    [Label("V")]
    [Description("Estimated score. Positive value for black, negative for white.")]
    public class GameScore: ADecimalValue<GameScore>
    {
        /// <summary>Initialize the property</summary>
        public GameScore(decimal value) : base(value) {}
    }
}
