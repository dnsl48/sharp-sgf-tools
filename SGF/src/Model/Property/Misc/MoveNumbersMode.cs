using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#PM
    /// </summary>
    [Label("PM")]
    [Description("Specifies how move numbers should be printed")]
    public class MoveNumbersMode : AIntegerValue<MoveNumbersMode>
    {
        /// <summary>Initialize the property</summary>
        public MoveNumbersMode(int value) : base(value) { }
    }
}
