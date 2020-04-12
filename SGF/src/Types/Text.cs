using System.Collections.Generic;

namespace dnsl48.SGF.Types
{
    /// <summary>
    /// Implementation of Text
    /// Defined by the SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#text
    /// </summary>
    public class Text : IPropertyValue
    {
        private string _source;

        /// <value>The source string value</value>
        public string source { get { return _source; } }

        /// <summary>
        /// Initialize with text value
        /// </summary>
        /// <param name="source">The value</param>
        public Text(string source) => _source = source;

        /// <summary>
        /// Encode the source value into SGF format
        /// </summary>
        protected string _encode()
        {
            return source
                .Replace("\\", "\\\\")
                .Replace("]", "\\]")
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
                .Replace("\\]", "]")
                .Replace("\\\\", "\\")
            ;
        }

        /// <summary>
        /// Parse an SGF encoded string
        /// </summary>
        /// <param name="value">SGF encoded text value</param>
        /// <returns>A new instance of the SimpleComposedText</returns>
        public static Text parse(string value) { return new Text(_decode(value)); }
    }
}
