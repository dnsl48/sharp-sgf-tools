using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Move
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#B
    /// </summary>
    [Label("B")]
    [Description("Black player move")]
    [Colour(Colour.Black)]
    public class BlackMove : ColouredMove<BlackMove>
    {
        /// <summary>Initialize the property</summary>
        public BlackMove(byte x, byte y) : base(x, y) {}
    }
}
