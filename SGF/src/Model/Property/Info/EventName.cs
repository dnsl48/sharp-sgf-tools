using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#EV
    /// </summary>
    [Label("EV")]
    [Description("Name of the event (e.g. tournament)")]
    public class EventName : ASimpleTextValue<EventName>
    {
        /// <summary>Initialize the property</summary>
        public EventName(SimpleText name) : base(name) {}
    }
}
