using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Info;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property {
    public class GameResultSuite
    {
        [Fact]
        public void TestDraw()
        {
            Assert.Equal("RE[Draw]", new GameResult(GameResult.WinType.Draw).StringValue());
        }

        [Fact]
        public void TestForfeit()
        {
            Assert.Equal("RE[W+F]", new GameResult(GameResult.WinType.Forfeit, Colour.White).StringValue());
            Assert.Equal("RE[B+F]", new GameResult(GameResult.WinType.Forfeit, Colour.Black).StringValue());
        }

        [Fact]
        public void TestResign()
        {
            Assert.Equal("RE[W+R]", new GameResult(GameResult.WinType.Resign, Colour.White).StringValue());
            Assert.Equal("RE[B+R]", new GameResult(GameResult.WinType.Resign, Colour.Black).StringValue());
        }

        [Fact]
        public void TestTime()
        {
            Assert.Equal("RE[W+T]", new GameResult(GameResult.WinType.Time, Colour.White).StringValue());
            Assert.Equal("RE[B+T]", new GameResult(GameResult.WinType.Time, Colour.Black).StringValue());
        }

        [Fact]
        public void TestUnknown()
        {
            Assert.Equal("RE[?]", new GameResult(GameResult.WinType.Unknown).StringValue());
        }

        [Fact]
        public void TestVoid()
        {
            Assert.Equal("RE[Void]", new GameResult(GameResult.WinType.Void).StringValue());
        }

        [Fact]
        public void TestScore()
        {
            Assert.Equal("RE[B+0.5]", new GameResult(Colour.Black, 0.5m).StringValue());
            Assert.Equal("RE[B+3.5]", new GameResult(Colour.Black, 3.5m).StringValue());
            Assert.Equal("RE[W+55.5]", new GameResult(Colour.White, 55.5m).StringValue());
        }
    }
}
