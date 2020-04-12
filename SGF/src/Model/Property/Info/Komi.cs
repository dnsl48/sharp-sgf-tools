using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Info
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#KM
    /// </summary>
    [Label("KM")]
    [Description("Komi")]
    public class Komi : ADecimalValue<Komi>
    {
        /// <summary>Initialize the property</summary>
        public Komi(decimal value): base(value) {}
    }
}
