using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Annotation;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class InterestingMoveSuite
    {
        [Fact]
        public void TestStringValue()
        {
            Assert.Equal("IT[]", new InterestingMove().StringValue());
        }

        [Fact]
        public void TestParse()
        {
            Assert.Equal("IT[]", InterestingMove.Parse(new List<string>()).StringValue());
            Assert.Equal("IT[]", InterestingMove.Parse(new List<string>(new[] { "" })).StringValue());
            Assert.Equal("IT[]", InterestingMove.Parse(new List<string>(new[] { " " })).StringValue());
        }
    }
}
