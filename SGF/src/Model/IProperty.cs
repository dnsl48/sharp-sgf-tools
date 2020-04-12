namespace dnsl48.SGF.Model
{
    /// <summary>
    /// SGF node property interface
    /// </summary>
    public interface IProperty
    {
        /// <summary>Human friendly description of the property purpose</summary>
        string GetDescription();

        /// <summary>The label of the property defined by the SGF standard (identity)</summary>
        string GetLabel();

        /// <summary>SGF encoded representation of the property (including brackets [], label and values)</summary>
        string StringValue();
    }
}
