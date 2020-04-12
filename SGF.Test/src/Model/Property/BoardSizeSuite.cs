using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Root;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class BoardSizeSuite
    {
        [Fact]
        public void TestSimple()
        {
            Assert.Equal("SZ[19]", new BoardSize(19).StringValue());
            Assert.Equal("SZ[13]", new BoardSize(13, 13).StringValue());
            Assert.Equal("SZ[5:6]", new BoardSize(5, 6).StringValue());
        }

        [Theory]
        [InlineData("19", 19, 19)]
        [InlineData("13", 13, 13)]
        [InlineData("19:19", 19, 19)]
        [InlineData("19:18", 19, 18)]
        [InlineData("18:19", 18, 19)]
        public void TestParse(string source, int x, int y)
        {
            var prop = (BoardSize) BoardSize.Parse(new List<string>(new[] { source }));
            Assert.NotNull(prop);
            Assert.Equal(x, prop.x);
            Assert.Equal(y, prop.y);

            if (x == y) {
                Assert.Equal($"SZ[{x}]", prop.StringValue());
            } else {
                Assert.Equal($"SZ[{source}]", prop.StringValue());
            }
        }
    }
}
