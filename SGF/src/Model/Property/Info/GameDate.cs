using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#DT
    /// </summary>
    [Label("DT")]
    [Description("Date when the game was played")]
    public class GameDate : ASimpleTextValue<GameDate>
    {
        /// <summary>Initialize the property</summary>
        public GameDate(SimpleText value) : base(value) {}
    }
}
