using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#GB
    /// </summary>
    [Label("GB")]
    [Description("Something good for black")]
    [Colour(Colour.Black)]
    public class GoodForBlack: AColouredIntegerValue<GoodForBlack>
    {
        /// <summary>Initialize the property</summary>
        public GoodForBlack(int i): base(i) {}
    }
}
