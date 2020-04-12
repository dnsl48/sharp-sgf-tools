using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Move
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#W
    /// </summary>
    [Label("W")]
    [Description("White player move")]
    [Colour(Colour.White)]
    public class WhiteMove : ColouredMove<WhiteMove>
    {
        /// <summary>Initialize the property</summary>
        public WhiteMove(byte x, byte y) : base(x, y) {}
    }
}
