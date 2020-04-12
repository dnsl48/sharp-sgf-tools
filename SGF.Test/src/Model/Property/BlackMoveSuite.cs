using System.Collections.Generic;
using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Move;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class BlackMoveSuite
    {
        [Fact]
        public void TestColour()
        {
            Assert.Equal(Colour.Black, new BlackMove(0, 0).GetColour());
        }

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("aa", 1, 1)]
        [InlineData("ef", 5, 6)]
        [InlineData("zA", 26, 27)]
        [InlineData("Az", 27, 26)]
        [InlineData("ZZ", 52, 52)]
        public void TestValues(string source, byte x, byte y)
        {
            var parsed = (BlackMove) BlackMove.Parse(new List<string>(new[] { source }));
            var generated = new BlackMove(x, y);

            Assert.NotNull(parsed);

            Assert.Equal(x, parsed.GetX());
            Assert.Equal(y, parsed.GetY());
            Assert.Equal($"B[{source}]", parsed.StringValue());


            Assert.Equal(x, generated.GetX());
            Assert.Equal(y, generated.GetY());
            Assert.Equal($"B[{source}]", generated.StringValue());
        }

        [Fact]
        public void TestPass()
        {
            var parsed = (BlackMove) BlackMove.Parse(new List<string>(new[] { "" }));
            Assert.Equal("", parsed.GetValue());
            Assert.Equal(0, parsed.GetX());
            Assert.Equal(0, parsed.GetY());
        }
    }
}
