using System.Collections.Generic;
using System.Linq;

namespace dnsl48.SGF.Model
{
    /// <summary>
    /// Collection of the parsed trees of SGF data.
    /// </summary>
    public class Collection
    {
        private List<Tree> _trees;

        /// <value>
        /// The trees of data
        /// </value>
        public List<Tree> trees { get { return _trees; } }

        /// <summary>
        /// Init an empty collection
        /// </summary>
        public Collection()
        {
            _trees = new List<Tree>();
        }

        /// <summary>
        /// Init a collection of trees
        /// </summary>
        public Collection(IEnumerable<Tree> trees)
        {
            _trees = new List<Tree>();
            _trees.AddRange(trees);
        }
    }
}
