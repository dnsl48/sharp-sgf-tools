using System.Collections.Generic;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Interface for properties containing a set of vectors
    /// </summary>
    public interface IVectorSet
    {
        /// <summary>
        /// Returns iterable set of the vector values
        /// </summary>
        /// <returns>The set of vectors</returns>
        IEnumerable<(Point a, Point b)> GetVectorSet();
    }
}
