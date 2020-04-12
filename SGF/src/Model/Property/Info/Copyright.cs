using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#CP
    /// </summary>
    [Label("CP")]
    [Description("Any copyright information")]
    public class Copyright: ASimpleTextValue<Copyright>
    {
        /// <summary>Initialize the property</summary>
        public Copyright(SimpleText value): base(value) {}
    }
}
