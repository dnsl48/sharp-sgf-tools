using System.Collections.Generic;
using static dnsl48.SGF.Types.Position;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property containing a single position
    /// </summary>
    public abstract class APositionValue<T> : AProperty, IPosition
    where T : APositionValue<T>
    {
        private byte _x;

        private byte _y;

        /// <summary>
        /// Initialize with the values
        /// </summary>
        /// <param name="x">The X of the position</param>
        /// <param name="y">The Y of the position</param>
        public APositionValue(byte x, byte y) => (_x, _y) = (x, y);

        /// <inheritdoc />
        public byte GetX()
        {
            return _x;
        }

        /// <inheritdoc />
        public byte GetY()
        {
            return _y;
        }

        /// <inheritdoc />
        public string GetValue()
        {
            string x;
            string y;

            if (_x == 0)
                x = "";
            else
                x = ItoC(_x).ToString();

            if (_y == 0)
                y = "";
            else
                y = ItoC(_y).ToString();

            return $"{x}{y}";
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[{GetValue()}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count != 1)
                return null;

            var ctor = typeof(T).GetConstructor(new[] { typeof(byte), typeof(byte) });
            if (ctor == null)
                return null;

            if (values[0].Trim() == "")
                return (IProperty)ctor.Invoke(new object[] { (byte)0, (byte)0 });

            var point = ReadPoint(values[0]);

            if (point.x == 0 || point.y == 0)
                return null;

            try
            {
                return (IProperty)ctor.Invoke(new object[] { point.x, point.y });
            }
            catch (System.Exception) { return null; }
        }
    }
}
