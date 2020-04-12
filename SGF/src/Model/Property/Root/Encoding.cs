using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#CA
    /// </summary>
    [Label("CA")]
    [Description("Character encoding (for SimpleText and Text type)")]
    public class Encoding : ASimpleTextValue<Encoding>
    {
        /// <summary>Initialize the property</summary>
        public Encoding(SimpleText name) : base(name) {}
    }
}
