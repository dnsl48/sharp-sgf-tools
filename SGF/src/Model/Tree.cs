using System.Collections.Generic;

namespace dnsl48.SGF.Model
{
    /// <summary>
    /// Model of an SGF tree
    /// </summary>
    public class Tree
    {
        private List<Node> _nodes;

        /// <value>
        /// The tree nodes
        /// </value>
        public List<Node> nodes { get { return _nodes; } }

        private List<Tree> _children;

        /// <value>
        /// The children trees
        /// </value>
        public List<Tree> children { get { return _children; } }

        /// <summary>
        /// Initialize the tree
        /// </summary>
        public Tree(IEnumerable<Node> nodes, IEnumerable<Tree> children)
        {
            _children = new List<Tree>();
            _children.AddRange(children);

            _nodes = new List<Node>();
            _nodes.AddRange(nodes);
        }
    }
}
