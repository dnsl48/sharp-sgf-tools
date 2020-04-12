using System;
using System.Collections.Generic;
using System.Linq;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a set of positions
    /// </summary>
    public abstract class APositionSet<T> : AProperty, IPositionSet
    where T : APositionSet<T>
    {
        private List<(byte x, byte y)> _positions;

        /// <summary>
        /// Initialize with the values
        /// </summary>
        /// <param name="positions">The values to initialize with</param>
        public APositionSet(IEnumerable<(byte x, byte y)> positions) =>
            _positions = new List<(byte x, byte y)>(positions);

        /// <inheritdoc />
        public IEnumerable<(byte x, byte y)> GetPositions() => _positions;

        /// <inheritdoc />
        public override string StringValue()
        {
            var lst = Compress(_positions.Select(t => new Point(t.x, t.y))).Select(sqs =>
            {
                if (sqs.a.x == sqs.b.x && sqs.a.y == sqs.b.y)
                    return $"{ItoC(sqs.a.x)}{ItoC(sqs.a.y)}";
                else
                    return $"{ItoC(sqs.a.x)}{ItoC(sqs.a.y)}:{ItoC(sqs.b.x)}{ItoC(sqs.b.y)}";
            });

            return $"{GetLabel()}[{String.Join("][", lst.ToArray())}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            var ctor = typeof(T).GetConstructor(new[] { typeof(IEnumerable<(byte x, byte y)>) });
            if (ctor == null)
                return null;

            var points = values
            .SelectMany<string, string, (byte x, byte y)>(
                value =>
                {
                    var vls = value.Split(':');

                    if (vls.Length == 1)
                        return new[] { value };

                    if (vls.Length != 2)
                        return new[] { "" };

                    var p1 = ReadPoint(vls[0]);
                    var p2 = ReadPoint(vls[1]);

                    byte x1 = Math.Min(p1.x, p2.x);
                    byte x2 = Math.Max(p1.x, p2.x);
                    byte y1 = Math.Min(p1.y, p2.y);
                    byte y2 = Math.Max(p1.y, p2.y);

                    var _points = new List<string>();
                    for (var x = x1; x <= x2; ++x)
                    {
                        for (var y = y1; y <= y2; ++y)
                        {
                            _points.Add($"{ItoC(x)}{ItoC(y)}");
                        }
                    }

                    return _points;
                },
                (src, value) =>
                {
                    var val = value.Trim();
                    if (val == "")
                        return (x: 0, y: 0);
                    var point = ReadPoint(val);
                    return (x: point.x, y: point.y);
                }
            )
            .Where(p => p.x != 0u && p.y != 0u);

            try
            {
                return (IProperty)ctor.Invoke(new object[] { points });
            }
            catch (System.Exception) { return null; }
        }
    }
}
