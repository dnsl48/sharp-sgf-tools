using System.Collections.Generic;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Interface for properties containing a set of positions
    /// </summary>
    public interface IPositionSet
    {
        /// <summary>
        /// Returns iterable set of the position values
        /// </summary>
        /// <returns>The set of positions</returns>
        IEnumerable<(byte x, byte y)> GetPositions();
    }
}
