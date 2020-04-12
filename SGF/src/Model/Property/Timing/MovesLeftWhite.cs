using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Timing
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#OW
    /// </summary>
    [Label("OW")]
    [Description("Number of white moves left to play in this byo-yomi period")]
    [Colour(Colour.White)]
    public class MovesLeftWhite: AColouredIntegerValue<MovesLeftWhite>
    {
        /// <summary>Initialize the property</summary>
        public MovesLeftWhite(int time): base(time) {}
    }
}
