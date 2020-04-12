using System;
using System.Collections.Generic;
using System.Linq;

namespace dnsl48.SGF.Types
{
    /// <summary>
    /// Implements a primitive of a position point and some
    /// methods for working with it.
    /// SGF4 specification defines how the board positions should
    /// be encoded: https://www.red-bean.com/sgf/go.html#types
    /// </summary>
    public static class Position
    {
        /// <summary>
        /// Defines a point on the game board
        /// encoded by X and Y coordinates
        /// </summary>
        public struct Point: IComparable
        {
            /// <value>X coordinate</value>
            public byte x;

            /// <value>Y coordinate</value>
            public byte y;

            /// <summary>
            /// Initialize the point with the coordinates
            /// </summary>
            /// <param name="x">X coordinate</param>
            /// <param name="y">Y coordinate</param>
            public Point(byte x, byte y) => (this.x, this.y) = (x, y);

            /// <summary>
            /// Encode the point into SGF format
            /// </summary>
            /// <returns>Encoded SGF value of the position point</returns>
            public string StringValue()
            {
                return $"{ItoC(x)}{ItoC(y)}";
            }

            /// <summary>
            /// Check if the position is empty (has not been
            /// properly initialized)
            /// </summary>
            /// <returns>True if the position has not been initialized properly</returns>
            public bool Empty()
            {
                return x == 0 || y == 0;
            }

            /// <summary>
            /// Compare to another position and check by our internal rules,
            /// which are not defined in any way by the SGF specification.
            /// Currently we check X first an then Y.
            /// </summary>
            /// <returns>-1 if less, 0 if equals, 1 is greater</returns>
            public int CompareTo(object obj)
            {
                Point o = (Point) obj;

                if (x > o.x)
                    return 1;

                if (x < o.x)
                    return -1;

                if (y > o.y)
                    return 1;

                if (y < o.y)
                    return -1;

                return 0;
            }
        }

        /// <summary>
        /// Convert integer representation of an axis into its textual alternative
        /// </summary>
        /// <remarks>
        /// The conversion rules are defined by the SGF4 specification: https://www.red-bean.com/sgf/go.html#types
        /// </remarks>
        /// <param name="value">Integer value of a position axis</param>
        /// <returns>Textual representation of the position axis</returns>
        public static char ItoC(byte value)
        {
            char result;

            if (value > 26)
                result = (char)((byte)'A' + (value - 27));

            else
                result = (char)((byte)'a' + value - 1);

            return result;
        }

        /// <summary>
        /// Convert textual representation of an axis into its integer alternative
        /// </summary>
        /// <remarks>
        /// The conversion rules are defined by the SGF4 specification: https://www.red-bean.com/sgf/go.html#types
        /// </remarks>
        /// <param name="cval">Textual value of a position axis</param>
        /// <returns>Integer representation of the position axis</returns>
        public static byte CtoI(char cval)
        {
            byte result;

            byte value = (byte)cval;

            if (value >= (byte)'A' && value <= (byte)'Z')
                result = (byte)(value - ((byte)'A') + 27);

            else if (value >= (byte)'a' && value <= (byte)'z')
                result = (byte)(value - (byte)'a' + 1);

            else
                result = 0;

            return result;
        }

        /// <summary>
        /// Check if the character lies within range of the possible values 
        /// </summary>
        /// <param name="value">Textual representation of a position axis</param>
        /// <returns>True if it's valid</returns>
        public static bool CharInRange(char value)
        {
            if (value < 'A')
                return false;

            if (value < 'a' && value > 'Z')
                return false;

            if (value > 'z')
                return false;

            return true;
        }

        /// <summary>
        /// Buiild a Point from a SGF4 encoded position
        /// </summary>
        /// <param name="value">Textual representation of a position</param>
        /// <returns>A new position; Initialized with empty values if the value is incorrect</returns>
        public static Point ReadPoint(string value)
        {
            var chars = value.Trim().ToCharArray();
            if (chars.Length != 2)
                return new Point(0, 0);

            var x = value[0];
            var y = value[1];

            if (!CharInRange(x) || !CharInRange(y))
                return new Point(0, 0);

            return new Point(CtoI(x), CtoI(y));
        }

        /// <summary>
        /// Compresses a list of points
        /// The compression is defined by the SGF4 specification: https://www.red-bean.com/sgf/sgf4.html#3.5.1
        /// As an example, "[aa][ab][ac][ad]" may be compressed as "[aa:ad]"
        /// </summary>
        /// <param name="sourcePoints">A list of points to compress</param>
        /// <returns>A list of point tuples describing the compresed sets</returns>
        public static List<(Point a, Point b)> Compress(IEnumerable<Point> sourcePoints)
        {
            var points = new SortedSet<Point>(sourcePoints);
            var squares = new List<(Point, Point)>();
            var squared = new HashSet<Point>();

            Func<Point, bool> invalid = p => squared.Contains(p) || !points.Contains(p);
            Func<(Point a, Point b), bool> validSquare = pts =>
            {
                (Point a, Point b) = pts;

                if (invalid(a) || invalid(b))
                    return false;

                for (var x = a.x; x <= b.x; ++x)
                {
                    for (var y = a.y; y <= b.y; ++y)
                    {
                        if (invalid(new Point(x, y)))
                            return false;
                    }
                }

                return true;
            };

            foreach (var point in points)
            {
                if (squared.Contains(point))
                    continue;

                var pt0 = point;
                var pt1 = point;
                var pt2 = point;

                // Going left
                while (true)
                {
                    if (pt1.x > 1 && pt1.y > 1)
                    {
                        var tryPoint = new Point((byte)(pt1.x - 1), (byte)(pt1.y - 1));

                        if (validSquare((tryPoint, pt0)))
                        {
                            pt1 = tryPoint;
                            continue;
                        }
                    }

                    // try X axis
                    if (pt1.x > 1)
                    {
                        var tryPoint = new Point((byte)(pt1.x - 1), pt1.y);

                        if (validSquare((tryPoint, pt0)))
                        {
                            pt1 = tryPoint;
                            continue;
                        }
                    }

                    // try Y axis
                    if (pt1.y > 1)
                    {
                        var tryPoint = new Point(pt1.x, (byte)(pt1.y - 1));

                        if (validSquare((tryPoint, pt0)))
                        {
                            pt1 = tryPoint;
                            continue;
                        }
                    }

                    break;
                }

                // Going right
                while (true)
                {
                    // try both X and Y axis
                    if (pt2.x < 255 && pt2.y < 255)
                    {
                        var tryPoint = new Point((byte)(pt2.x + 1), (byte)(pt2.y + 1));

                        if (validSquare((pt1, tryPoint)))
                        {
                            pt2 = tryPoint;
                            continue;
                        }
                    }

                    // try X axis
                    if (pt2.x < 255) {
                        var tryPoint = new Point((byte)(pt2.x + 1), pt2.y);

                        if (validSquare((pt1, tryPoint)))
                        {
                            pt2 = tryPoint;
                            continue;
                        }
                    }

                    // try Y axis
                    if (pt2.y < 255)
                    {
                        var tryPoint = new Point(pt2.x, (byte)(pt2.y + 1));

                        if (validSquare((pt1, tryPoint)))
                        {
                            pt2 = tryPoint;
                            continue;
                        }
                    }

                    break;
                }

                squares.Add((pt1, pt2));

                for (var x = pt1.x; x <= pt2.x; ++x)
                {
                    for (var y = pt1.y; y <= pt2.y; ++y)
                    {
                        squared.Add(new Point(x, y));
                    }
                }
            }

            return squares;
        }
    }
}
