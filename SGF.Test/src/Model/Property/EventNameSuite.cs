using dnsl48.SGF.Model.Property.Info;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class EventNameSuite
    {
        [Fact]
        public void TestName()
        {
            Assert.Equal("Event name", new EventName(new SimpleText("Event name")).GetValue().source);
        }

        [Fact]
        public void TestStringValue()
        {
            Assert.Equal("EV[The name]", new EventName(new SimpleText("The name")).StringValue());
        }
    }
}
