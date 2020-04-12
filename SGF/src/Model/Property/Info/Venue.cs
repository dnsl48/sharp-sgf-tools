using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#PC
    /// </summary>
    [Label("PC")]
    [Description("The place where the game was played")]
    public class Venue: ASimpleTextValue<Venue>
    {
        /// <summary>Initialize the property</summary>
        public Venue(SimpleText value): base(value) {}
    }
}
