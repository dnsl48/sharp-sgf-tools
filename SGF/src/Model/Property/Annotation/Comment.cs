using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#C
    /// </summary>
    [Label("C")]
    [Description("Comment")]
    public class Comment : ATextValue<Comment>
    {
        /// <summary>Initialize the property</summary>
        public Comment(Text value) : base(value) {}
    }
}
