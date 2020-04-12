using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Setup;
using dnsl48.SGF.Model;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class PlayerTurnSuite
    {
        [Fact]
        public void TestColour()
        {
            Assert.Equal(Colour.White, new PlayerTurn(Colour.White).GetColour());
            Assert.Equal(Colour.Black, new PlayerTurn(Colour.Black).GetColour());
        }

        [Fact]
        public void TestStringValue()
        {
            Assert.Equal("PL[W]", new PlayerTurn(Colour.White).StringValue());
            Assert.Equal("PL[B]", new PlayerTurn(Colour.Black).StringValue());
        }

        [Fact]
        public void TestParser()
        {
            Assert.Equal(Colour.White, ((PlayerTurn) PlayerTurn.Parse(new List<string>(new[] { "W" }))).GetColour());
            Assert.Equal(Colour.Black, ((PlayerTurn) PlayerTurn.Parse(new List<string>(new[] { "B" }))).GetColour());
            Assert.Equal(Colour.White, ((PlayerTurn) PlayerTurn.Parse(new List<string>(new[] { "w" }))).GetColour());
            Assert.Equal(Colour.Black, ((PlayerTurn) PlayerTurn.Parse(new List<string>(new[] { "b" }))).GetColour());

            Assert.Null(PlayerTurn.Parse(new List<string>(new[] { "E" })));
            Assert.Null(PlayerTurn.Parse(new List<string>(new[] { "B", "C" })));
        }
    }
}
