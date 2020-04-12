using dnsl48.SGF.Attributes;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#FF
    /// </summary>
    [Label("FF")]
    [Description("The used file format")]
    public class FileFormat: AIntegerValue<FileFormat>
    {
        /// <summary>Initialize the property</summary>
        public FileFormat(int value): base(value) {}
    }
}
