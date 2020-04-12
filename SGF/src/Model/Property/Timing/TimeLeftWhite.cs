using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Timing
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#WL
    /// </summary>
    [Label("WL")]
    [Description("Time left for white, in seconds, after the move was made")]
    [Colour(Colour.White)]
    public class TimeLeftWhite: AColouredDecimalValue<TimeLeftWhite>
    {
        /// <summary>Initialize the property</summary>
        public TimeLeftWhite(decimal time): base(time) {}
    }
}
