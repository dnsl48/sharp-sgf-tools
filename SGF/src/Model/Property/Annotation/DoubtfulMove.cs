using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#DO
    /// </summary>
    [Label("DO")]
    [Description("The played move is doubtful")]
    public class DoubtfulMove: AEmptyValue<DoubtfulMove>
    {
    }
}
