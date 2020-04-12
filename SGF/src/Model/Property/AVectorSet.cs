using System;
using System.Collections.Generic;
using System.Linq;
using dnsl48.SGF.Types;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a set of vectors
    /// </summary>
    public abstract class AVectorSet<T> : AProperty, IVectorSet
    where T : AVectorSet<T>
    {
        private List<(Point a, Point b)> _vectors;

        /// <summary>
        /// Initialize with the values
        /// </summary>
        /// <param name="vectors">The values to initialize with</param>
        public AVectorSet(IEnumerable<(Point a, Point b)> vectors) =>
            _vectors = new List<(Point a, Point b)>(vectors);

        /// <summary>
        /// Returns the vector set values
        /// </summary>
        /// <returns>The vector set</returns>
        public IEnumerable<(Point a, Point b)> GetVectorSet() => _vectors;

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[{String.Join("][", _vectors.Select(vec => $"{vec.a.StringValue()}:{vec.b.StringValue()}"))}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            var ctor = typeof(T).GetConstructor(new[] { typeof(IEnumerable<(Point a, Point b)>) });
            if (ctor == null)
                return null;

            var vectors = values
                .Select(src => {
                    var vls = src.Split(new[] {':'}, 2);

                    if (vls.Count() != 2)
                        return (a: new Point(0, 0), b: new Point(0, 0));

                    return (a: ReadPoint(vls[0]), b: ReadPoint(vls[1]));
                })
                .Where(v => !v.a.Empty() && !v.b.Empty())
                .OrderBy(a => a.a).ThenBy(a => a.b)
            ;

            try
            {
                return (IProperty)ctor.Invoke(new object[] { vectors });
            }
            catch (System.Exception) { return null; }
        }
    }
}
