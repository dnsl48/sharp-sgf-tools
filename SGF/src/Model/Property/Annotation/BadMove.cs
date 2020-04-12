using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#BM
    /// </summary>
    [Label("BM")]
    [Description("The played move is bad")]
    public class BadMove: AIntegerValue<BadMove>
    {
        /// <summary>Initialize the property</summary>
        public BadMove(int i): base(i) {}
    }
}
