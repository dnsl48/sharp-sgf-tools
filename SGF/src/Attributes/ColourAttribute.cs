using dnsl48.SGF.Model;
using System;

namespace dnsl48.SGF.Attributes
{
    /// <summary>
    /// Information about a colour of the subject.
    /// </summary>
    /// <example>
    /// E.g. property [B] implies black move, so
    /// we can define a Colour.Black statically by
    /// assigning this attribute to the class implementing
    /// the black move property.
    /// <see cref="dnsl48.SGF.Model.Property.Move.BlackMove" />
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    [Serializable]
    public class ColourAttribute : Attribute
    {
        private Colour _value;

        /// <summary>
        /// Init the attribute with a colour
        /// </summary>
        /// <param name="value">Colour of the attribute subject</param>
        public ColourAttribute(Colour value) => _value = value;

        /// <summary>
        /// Returns the colour assigned to the subject
        /// </summary>
        /// <returns>The colour assigned</returns>
        public Colour GetValue()
        {
            return _value;
        }
    }
}
