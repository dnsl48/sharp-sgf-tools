using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#ST
    /// </summary>
    [Label("ST")]
    [Description("Defines how variations and markup should be shown")]
    public class MarkupSettings : AIntegerValue<MarkupSettings>
    {
        /// <summary>Initialize the property</summary>
        public MarkupSettings(int value) : base(value) {}
    }
}
