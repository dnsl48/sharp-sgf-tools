using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#OT
    /// </summary>
    [Label("OT")]
    [Description("Describes the method used for overtime (byo-yomi)")]
    public class Overtime : ASimpleTextValue<Overtime>
    {
        /// <summary>Initialize the property</summary>
        public Overtime(SimpleText value) : base(value) {}
    }
}
