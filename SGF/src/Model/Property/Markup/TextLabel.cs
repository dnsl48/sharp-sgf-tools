using System;
using System.Collections.Generic;
using System.Linq;
using dnsl48.SGF.Attributes;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Markup
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#LB
    /// </summary>
    [Label("LB")]
    [Description("Writes text on the board")]
    public class TextLabel: AProperty
    {
        private SortedDictionary<Position.Point, SimpleComposedText> _data;

        /// <value>The list of points with text that should be drawn around it</value>
        public SortedDictionary<Position.Point, SimpleComposedText> points { get { return _data; } }

        /// <summary>Initialize the property</summary>
        public TextLabel(IEnumerable<(Position.Point point, SimpleComposedText text)> values)
        {
            _data = new SortedDictionary<Position.Point, SimpleComposedText>();
            foreach (var item in values)
                _data[item.point] = item.text;
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            var values = String.Join("][", _data.Select(a => $"{a.Key.StringValue()}:{a.Value.ToString()}"));
            return $"{GetLabel()}[{values}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count == 0)
                return null;

            var points = values
                .Select(src => {
                    var vls = src.Split(new[] {':'}, 2);

                    if (vls.Count() != 2)
                        return (position: new Position.Point(0, 0), text: new SimpleComposedText(""));

                    var pos = Position.ReadPoint(vls[0]);
                    var txt = SimpleComposedText.parse(vls[1]);

                    return (position: pos, text: txt);
                })
                .Where(a => !a.position.Empty());

            return new TextLabel(points);
        }
    }
}
