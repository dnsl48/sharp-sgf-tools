using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#SO
    /// </summary>
    [Label("SO")]
    [Description("Provides the name of the source (e.g. book, journal, ...)")]
    public class SourceName : ASimpleTextValue<SourceName>
    {
        /// <summary>Initialize the property</summary>
        public SourceName(SimpleText name) : base(name) {}
    }
}
