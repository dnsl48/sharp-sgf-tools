using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Info;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class BlackPlayerNameSuite
    {
        [Fact]
        public void TestColour()
        {
            Assert.Equal(Colour.Black, new BlackPlayerName(new SimpleText("Lee Sedol")).GetColour());
        }

        [Fact]
        public void TestName()
        {
            Assert.Equal("Lee Sedol", new BlackPlayerName(new SimpleText("Lee Sedol")).GetValue().source);
        }

        [Fact]
        public void TestValue()
        {
            Assert.Equal("PB[Lee Sedol]", new BlackPlayerName(new SimpleText("Lee Sedol")).StringValue());
        }
    }
}
