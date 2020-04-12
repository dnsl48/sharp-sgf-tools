using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Markup;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class TextLabelSuite
    {
        [Fact]
        public void TestConstruct()
        {
            var lbls = new TextLabel(new[] {
                (new Position.Point(1, 2), new SimpleComposedText("Test composed text 01")),
                (new Position.Point(2, 3), new SimpleComposedText("Test composed text 02")),
            });

            Assert.Equal(2, lbls.points.Count);

            var key = new Position.Point(1, 2);
            var val = "Test composed text 01";
            Assert.True(lbls.points.ContainsKey(key));
            Assert.Equal(val, lbls.points[key].source);

            key = new Position.Point(2, 3);
            val = "Test composed text 02";
            Assert.True(lbls.points.ContainsKey(key));
            Assert.Equal(val, lbls.points[key].source);
        }

        [Fact]
        public void TestStringValue()
        {
            var lbls = new TextLabel(new[] {
                (new Position.Point(1, 2), new SimpleComposedText("Test composed text 01")),
                (new Position.Point(2, 3), new SimpleComposedText("Test composed text 02")),
            });

            Assert.Equal("LB[ab:Test composed text 01][bc:Test composed text 02]", lbls.StringValue());
        }

        [Fact]
        public void TestParse()
        {
            var lbls = (TextLabel) TextLabel.Parse(new List<string> {
                "ab:Test composed text 01",
                "bc:Test composed text 02"
            });

            Assert.NotNull(lbls);
            Assert.Equal(2, lbls.points.Count);

            var key = new Position.Point(1, 2);
            var val = "Test composed text 01";
            Assert.True(lbls.points.ContainsKey(key));
            Assert.Equal(val, lbls.points[key].source);

            key = new Position.Point(2, 3);
            val = "Test composed text 02";
            Assert.True(lbls.points.ContainsKey(key));
            Assert.Equal(val, lbls.points[key].source);
        }
    }
}
