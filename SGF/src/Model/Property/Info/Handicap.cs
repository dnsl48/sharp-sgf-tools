using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#HA
    /// </summary>
    [Label("HA")]
    [Description("Handicap")]
    public class Handicap: AIntegerValue<Handicap>
    {
        /// <summary>Initialize the property</summary>
        public Handicap(int time): base(time) {}
    }
}
