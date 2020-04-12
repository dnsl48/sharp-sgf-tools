using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Timing
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#BL
    /// </summary>
    [Label("BL")]
    [Description("Time left for black, in seconds, after the move was made")]
    [Colour(Colour.Black)]
    public class TimeLeftBlack: AColouredDecimalValue<TimeLeftBlack>
    {
        /// <summary>Initialize the property</summary>
        public TimeLeftBlack(decimal time): base(time) {}
    }
}
