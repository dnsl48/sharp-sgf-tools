namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Interface for properties containing a SimpleText value.
    /// SimpleText defined by SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#simpletext
    /// </summary>
    public interface ISimpleTextValue
    {
        /// <summary>
        /// Returns the property value
        /// </summary>
        /// <returns>The SimpleText value</returns>
        Types.SimpleText GetValue();
    }
}
