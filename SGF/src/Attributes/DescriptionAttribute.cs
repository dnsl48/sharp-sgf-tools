using System;

namespace dnsl48.SGF.Attributes
{
    /// <summary>
    /// User-friendly description of a subject.
    /// Usually would contain a description given by the SGF standard introducing the
    /// subject parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    [Serializable]
    public class DescriptionAttribute : Attribute
    {
        private string _value;

        /// <summary>
        /// Initialize the attribute with the description
        /// </summary>
        /// <param name="value">Description of the subject</param>
        public DescriptionAttribute(string value) => _value = value;

        /// <inheritdoc/>
        public override string ToString()
        {
            return _value;
        }
    }
}
