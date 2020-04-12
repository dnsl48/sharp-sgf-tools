using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Annotation
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#GW
    /// </summary>
    [Label("GW")]
    [Description("Something good for white")]
    [Colour(Colour.White)]
    public class GoodForWhite: AColouredIntegerValue<GoodForWhite>
    {
        /// <summary>Initialize the property</summary>
        public GoodForWhite(int i): base(i) {}
    }
}
