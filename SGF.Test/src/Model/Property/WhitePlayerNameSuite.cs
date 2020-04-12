using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Info;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class WhitePlayerNameSuite
    {
        [Fact]
        public void TestColour()
        {
            Assert.Equal(Colour.White, new WhitePlayerName(new SimpleText("Lee Sedol")).GetColour());
        }

        [Fact]
        public void TestName()
        {
            Assert.Equal("Lee Sedol", new WhitePlayerName(new SimpleText("Lee Sedol")).GetValue().source);
        }

        [Fact]
        public void TestValue()
        {
            Assert.Equal("PW[Lee Sedol]", new WhitePlayerName(new SimpleText("Lee Sedol")).StringValue());
        }
    }
}
