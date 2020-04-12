using System;

namespace dnsl48.SGF.Attributes
{
    /// <summary>
    /// The label of a subject property.
    /// The label is the identity of the property defined by the SGF standard
    /// and being used in the SGF document as is. It is also being used by the
    /// parser to identify the property and apply the appropriate parsing logic
    /// to its values and parameters.
    /// </summary>
    /// <example>
    /// Smart Game Format defines the property "B" as "Execute a black move".
    /// Its label in this case is "B" and the description is "Execute a black move".
    /// See: https://www.red-bean.com/sgf/properties.html#B
    /// <see cref="dnsl48.SGF.Model.Property.Move.BlackMove" />
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    [Serializable]
    public class LabelAttribute : Attribute
    {
        private string _value;

        /// <summary>
        /// Initialize the attribute with the subject label
        /// </summary>
        /// <param name="value">The label string representation</param>
        public LabelAttribute(string value) => _value = value;

        /// <inheritdoc />
        public override string ToString()
        {
            return _value;
        }
    }
}
