using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AN
    /// </summary>
    [Label("AN")]
    [Description("Name of the person, who made the annotations")]
    public class AnnotationProvider : ASimpleTextValue<AnnotationProvider>
    {
        /// <summary>Initialize the property</summary>
        public AnnotationProvider(SimpleText value) : base(value) {}
    }
}
