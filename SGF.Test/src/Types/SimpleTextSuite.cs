using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Types
{
    public class SimpleTextSuite
    {
        [Fact]
        public void TestSource()
        {
            var source = "This is the test";

            Assert.Equal(source, new SimpleText(source).source);
        }

        [Fact]
        public void TestText()
        {
            var source = "This text mustn't be affected with any formatting";

            Assert.Equal(source, new SimpleText(source).ToString());
        }

        [Theory]
        [InlineData("a\nb", "a b")]
        [InlineData("a\rb", "a b")]
        [InlineData("a\r\nb", "a b")]
        [InlineData("a\n\rb", "a b")]
        [InlineData("a]b", "a\\]b")]
        [InlineData("a\\b", "a\\\\b")]
        // [InlineData("a:b", "a\\:b")]
        public void TestFormatNewlines(string source, string result)
        {
            Assert.Equal(result, new SimpleText(source).ToString());
        }

        [Theory]
        [InlineData("a\\]b", "a]b")]
        [InlineData("a\\\\b", "a\\b")]
        // [InlineData("a\\:b", "a:b")]
        public void TestDecode(string source, string result)
        {
            Assert.Equal(result, SimpleText.parse(source).source);
            Assert.Equal(source, SimpleText.parse(source).ToString());
        }
    }
}
