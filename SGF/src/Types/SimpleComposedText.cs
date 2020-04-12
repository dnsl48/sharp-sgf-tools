namespace dnsl48.SGF.Types
{
    /// <summary>
    /// SimpleComposedText is same as SimpleText, but handling ':' character properly
    /// <seealso cref="SimpleText" />
    /// </summary>
    public class SimpleComposedText : IPropertyValue
    {
        private string _source;

        /// <value>The source string value</value>
        public string source { get { return _source; } }

        /// <summary>
        /// Initialize with text value
        /// </summary>
        /// <param name="source">The value</param>
        public SimpleComposedText(string source) => _source = source;

        /// <summary>
        /// Encode the source value into SGF format
        /// </summary>
        protected string _encode()
        {
            return source
                .Replace("\\", "\\\\")
                .Replace("]", "\\]")
                .Replace(":", "\\:")
                .Replace("\n\r", " ")
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\r", " ")
            ;
        }

        /// <inheritdoc />
        public override string ToString() { return _encode(); }

        /// <summary>
        /// Decode SGF value
        /// </summary>
        /// <param name="value">SGF encoded string</param>
        protected static string _decode(string value)
        {
            return value
                .Replace("\\\n\r", "")
                .Replace("\\\r\n", "")
                .Replace("\\\n", "")
                .Replace("\\\r", "")
                .Replace("\r\n", " ")
                .Replace("\n\r", " ")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("\\\\", "\\")
                .Replace("\\:", ":")
                .Replace("\\]", "]")
            ;
        }

        /// <summary>
        /// Parse an SGF encoded string
        /// </summary>
        /// <param name="value">SGF encoded text value</param>
        /// <returns>A new instance of the SimpleComposedText</returns>
        public static SimpleComposedText parse(string value)
        {
            return new SimpleComposedText(_decode(value));
        }
    }
}
