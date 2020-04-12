using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#RU
    /// </summary>
    [Label("RU")]
    [Description("Game Rules")]
    public class Rules : ASimpleTextValue<Rules>
    {
        /// <summary>Initialize the property</summary>
        public Rules(SimpleText name) : base(name) {}
    }
}
