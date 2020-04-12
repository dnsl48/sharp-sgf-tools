using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Root;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class ApplicationSuite
    {
        [Fact]
        public void TestSimple()
        {
            Assert.Equal("AP[App:123]", new Application(new SimpleComposedText("App"), new SimpleComposedText("123")).StringValue());
            Assert.Equal("AP[Ap\\:p:12\\]3]", new Application(new SimpleComposedText("Ap:p"), new SimpleComposedText("12]3")).StringValue());
        }

        [Theory]
        [InlineData("Application:version", "Application", "version")]
        [InlineData("A\\:B:C\\:D", "A:B", "C:D")]
        [InlineData("Ap\\:p:12\\]3", "Ap:p", "12]3")]
        [InlineData("CGoban:1.6.2", "CGoban", "1.6.2")]
        [InlineData("Many Faces of Go:10.0", "Many Faces of Go", "10.0")]
        public void TestParse(string source, string app, string version)
        {
            var prop = (Application) Application.Parse(new List<string>(new[] { source }));
            Assert.NotNull(prop);
            Assert.Equal(app, prop.app.source);
            Assert.Equal(version, prop.version.source);
            Assert.Equal($"AP[{source}]", prop.StringValue());
        }
    }
}
