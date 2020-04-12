using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#DM
    /// </summary>
    [Label("DM")]
    [Description("The players position is even")]
    public class EvenPosition: AIntegerValue<EvenPosition>
    {
        /// <summary>Initialize the property</summary>
        public EvenPosition(int i): base(i) {}
    }
}
