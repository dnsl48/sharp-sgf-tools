namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Interface for properties containing a Text value.
    /// Text defined by SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#text
    /// </summary>
    public interface ITextValue
    {
        /// <summary>
        /// Returns the property value
        /// </summary>
        /// <returns>The Text value</returns>
        Types.Text GetValue();
    }
}
