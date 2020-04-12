using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Markup;
using static dnsl48.SGF.Types.Position;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class ArrowMarkerSuite
    {
        [Fact]
        public void TestConstruct()
        {
            var arrows = new ArrowMarker(new List<(Point a, Point b)> {
                (new Point(1, 2), new Point(3, 4)),
                (new Point(8, 7), new Point(6, 5))
            });

            var vecs = new List<(Point a, Point b)> (arrows.GetVectorSet());

            Assert.Equal(2, vecs.Count);

            Assert.Equal(1, vecs[0].a.x);
            Assert.Equal(2, vecs[0].a.y);
            Assert.Equal(3, vecs[0].b.x);
            Assert.Equal(4, vecs[0].b.y);

            Assert.Equal(8, vecs[1].a.x);
            Assert.Equal(7, vecs[1].a.y);
            Assert.Equal(6, vecs[1].b.x);
            Assert.Equal(5, vecs[1].b.y);
        }

        [Fact]
        public void TestStringValue()
        {
            var arrows = new ArrowMarker(new List<(Point a, Point b)> {
                (new Point(1, 2), new Point(3, 4)),
                (new Point(8, 7), new Point(6, 5))
            });

            Assert.Equal("AR[ab:cd][hg:fe]", arrows.StringValue());
        }

        [Fact]
        public void TestParse()
        {
            var arrows = (ArrowMarker) ArrowMarker.Parse(new List<string> {
                "ab:cd",
                "hg:fe"
            });

            Assert.NotNull(arrows);

            var vecs = new List<(Point a, Point b)> (arrows.GetVectorSet());

            Assert.Equal(2, vecs.Count);

            Assert.Equal(1, vecs[0].a.x);
            Assert.Equal(2, vecs[0].a.y);
            Assert.Equal(3, vecs[0].b.x);
            Assert.Equal(4, vecs[0].b.y);

            Assert.Equal(8, vecs[1].a.x);
            Assert.Equal(7, vecs[1].a.y);
            Assert.Equal(6, vecs[1].b.x);
            Assert.Equal(5, vecs[1].b.y);
        }
    }
}
