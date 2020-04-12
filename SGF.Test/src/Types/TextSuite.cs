using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests
{
    public class TextSuite
    {
        [Fact]
        public void TestSource()
        {
            var source = "This is the test";

            Assert.Equal(source, new Text(source).source);
        }

        [Fact]
        public void TestText()
        {
            var source = "This text mustn't be affected with any formatting";

            Assert.Equal(source, new Text(source).ToString());
        }

        [Theory]
        [InlineData("a\nb", "a\nb")]
        [InlineData("a\rb", "a\rb")]
        [InlineData("a\r\nb", "a\r\nb")]
        [InlineData("a\n\rb", "a\n\rb")]
        [InlineData("a]b", "a\\]b")]
        [InlineData("a\\b", "a\\\\b")]
        public void TestFormatNewlines(string source, string result)
        {
            Assert.Equal(result, new Text(source).ToString());
        }

        [Theory]
        [InlineData("a\\]b", "a]b")]
        [InlineData("a\\\\b", "a\\b")]
        public void TestDecode(string source, string result)
        {
            Assert.Equal(result, Text.parse(source).source);
            Assert.Equal(source, Text.parse(source).ToString());
        }
    }
}
