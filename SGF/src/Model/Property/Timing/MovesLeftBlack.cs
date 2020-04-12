using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Timing
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#OB
    /// </summary>
    [Label("OB")]
    [Description("Number of black moves left to play in this byo-yomi period")]
    [Colour(Colour.Black)]
    public class MovesLeftBlack: AColouredIntegerValue<MovesLeftBlack>
    {
        /// <summary>Initialize the property</summary>
        public MovesLeftBlack(int time): base(time) {}
    }
}
