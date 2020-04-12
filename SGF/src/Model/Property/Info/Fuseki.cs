using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#ON
    /// </summary>
    [Label("ON")]
    [Description("Info about fuseki (opening) played")]
    public class Fuseki : ASimpleTextValue<Fuseki>
    {
        /// <summary>Initialize the property</summary>
        public Fuseki(SimpleText name) : base(name) {}
    }
}
