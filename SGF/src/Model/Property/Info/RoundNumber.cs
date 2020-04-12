using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#RO
    /// </summary>
    [Label("RO")]
    [Description("Round-number and type of round")]
    public class RoundNumber : ASimpleTextValue<RoundNumber>
    {
        /// <summary>Initialize the property</summary>
        public RoundNumber(SimpleText round) : base(round) { }
    }
}
