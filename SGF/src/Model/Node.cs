using System;
using System.Collections.Generic;
using System.Linq;

namespace dnsl48.SGF.Model
{
    /// <summary>
    /// Model of an SGF node
    /// </summary>
    public class Node
    {
        private Dictionary<string, IProperty> _properties;

        /// <value>
        /// The node properties
        /// </value>
        public Dictionary<string, IProperty> props { get { return _properties; } }

        /// <summary>
        /// Initialize the node with properties
        /// </summary>
        public Node(IEnumerable<IProperty> properties) {
            _properties = new Dictionary<string, IProperty>();

            foreach (var item in properties)
                _properties[item.GetLabel()] = item;
        }

        /// <summary>
        /// Generate SGF representation of the node
        /// </summary>
        public override string ToString() => ";" + String.Join(" ", _properties.Select(pair => pair.Value.StringValue()));
    }
}
