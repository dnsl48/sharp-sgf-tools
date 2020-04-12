using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#N
    /// </summary>
    [Label("N")]
    [Description("Name of the node")]
    public class NodeName : ASimpleTextValue<NodeName>
    {
        /// <summary>Initialize the property</summary>
        public NodeName(SimpleText value): base(value) {}
    }
}
