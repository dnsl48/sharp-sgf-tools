using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#HO
    /// </summary>
    [Label("HO")]
    [Description("Something interesting, e.g. game-deciding move")]
    public class Hotspot: AIntegerValue<Hotspot>
    {
        /// <summary>Initialize the property</summary>
        public Hotspot(int i): base(i) {}
    }
}
