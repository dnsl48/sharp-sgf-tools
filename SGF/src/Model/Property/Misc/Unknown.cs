using System.Collections.Generic;
using System.Linq;
using System;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A property lacking implementation in the library.
    /// The parsers should use this model to preserve lexemes
    /// and allow the application to read and apply its own logic.
    /// </summary>
    public class Unknown : IProperty
    {
        private string _label;

        private List<string> _values;

        /// <summary>Initialize the property</summary>
        public Unknown(string label, List<string> values) =>
            (_label, _values) = (label, values);

        /// <inheritdoc />
        public string GetLabel()
        {
            return _label;
        }

        /// <inheritdoc />
        public string GetDescription()
        {
            return "¯\\_(ツ)_/¯";
        }

        /// <inheritdoc />
        public string StringValue()
        {
            return $"{GetLabel()}[{String.Join("][", _values.ToArray())}]";
        }
    }
}
