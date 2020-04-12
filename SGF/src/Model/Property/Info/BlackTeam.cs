using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#BT
    /// </summary>
    [Label("BT")]
    [Description("Name of the black team")]
    [Colour(Colour.Black)]
    public class BlackTeam : AColouredSimpleTextValue<BlackTeam>
    {
        /// <summary>Initialize the property</summary>
        public BlackTeam(SimpleText value) : base(value) {}
    }
}
