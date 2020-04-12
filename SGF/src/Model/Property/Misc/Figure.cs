using System.Collections.Generic;
using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Misc
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#FG
    /// </summary>
    [Label("FG")]
    [Description("Specifies the figure for printing")]
    public class Figure: AProperty
    {
        private int _flags;
        private SimpleComposedText _name;

        /// <value>Flags as defined by the SGF specs</value>
        public int Flags { get { return _flags; } }

        /// <value>Name of the figure</value>
        public SimpleComposedText Name { get { return _name; } }

        /// <summary>Initialize the property</summary>
        public Figure(int flags, SimpleComposedText name)
        {
            _flags = flags;
            _name = name;
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[{_flags}:{_name.ToString()}]";
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

            var value = values[0].Split(new[] {':'}, 2);

            if (value.Length != 2)
                return null;

            var flags = 0;
            try {
                flags = int.Parse(value[0]);
            } catch (System.Exception) { return null; }

            var name = SimpleComposedText.parse(value[1]);

            return new Figure(flags, name);
        }
    }
}
