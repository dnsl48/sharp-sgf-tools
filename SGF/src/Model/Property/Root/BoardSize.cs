using System.Collections.Generic;
using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#SZ
    /// </summary>
    [Label("SZ")]
    [Description("Size of the board")]
    public class BoardSize : AProperty
    {
        /// <value>X axis</value>
        public int x { get; }

        /// <value>Y axis</value>
        public int y { get; }

        /// <summary>Initialize the property (square board)</summary>
        public BoardSize(int size) : this(size, size) { }

        /// <summary>Initialize the property</summary>
        public BoardSize(int x, int y) => (this.x, this.y) = (x, y);

        /// <inheritdoc />
        public override string StringValue()
        {
            if (x == y)
                return $"SZ[{x}]";
            else
                return $"SZ[{x}:{y}]";
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

            var value = values[0].Split(':');
            if (value.Length == 0 || value.Length > 2)
                return null;

            try
            {
                var x = int.Parse(value[0].Trim());

                if (value.Length == 2) {
                    var y = int.Parse(value[1].Trim());
                    return new BoardSize(x, y);
                } else {
                    return new BoardSize(x);
                }
            }
            catch (System.Exception) { return null; }
        }
    }
}
