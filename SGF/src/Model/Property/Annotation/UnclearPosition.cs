using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#UC
    /// </summary>
    [Label("UC")]
    [Description("The position is unclear")]
    public class UnclearPosition: AIntegerValue<UnclearPosition>
    {
        /// <summary>Initialize the property</summary>
        public UnclearPosition(int i): base(i) {}
    }
}
