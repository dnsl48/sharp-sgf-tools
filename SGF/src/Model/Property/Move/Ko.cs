using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Move
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#KO
    /// </summary>
    [Label("KO")]
    [Description("Execute a given move even it's illegal")]
    public class Ko: AEmptyValue<Ko>
    {
    }
}
