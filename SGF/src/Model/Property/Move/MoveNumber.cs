using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Move
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#MN
    /// </summary>
    [Label("MN")]
    [Description("Sets the move number to the given value")]
    public class MoveNumber: AIntegerValue<MoveNumber>
    {
        /// <summary>Initialize the property</summary>
        public MoveNumber(int value): base(value) {}
    }
}
