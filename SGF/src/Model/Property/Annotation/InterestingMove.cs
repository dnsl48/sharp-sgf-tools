using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#IT
    /// </summary>
    [Label("IT")]
    [Description("The played move is interesting")]
    public class InterestingMove: AEmptyValue<InterestingMove>
    {
    }
}
