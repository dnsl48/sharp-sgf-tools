using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#TE
    /// </summary>
    [Label("TE")]
    [Description("The played move is a tesuji (good move)")]
    public class GoodMove: AIntegerValue<GoodMove>
    {
        /// <summary>Initialize the property</summary>
        public GoodMove(int i): base(i) {}
    }
}
