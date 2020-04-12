using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Root;
using dnsl48.SGF.Model.Property.Info;
using dnsl48.SGF.Types;

using Xunit;


namespace dnsl48.SGF.Tests.Model
{
    public class CollectionSuite
    {
        [Fact]
        public void TestCollectionToString()
        {
            var node = new Node(new IProperty[] {
                (IProperty) new Application(new SimpleComposedText("Sharp SGF tools"), new SimpleComposedText("0.0.1")),
                (IProperty) new GameResult(Colour.Black, 8.5m),
                (IProperty) new GameType(1)
            });
            var tree = new Tree(new Node[] { node }, new Tree[] {});
            var collection = new Collection(new Tree[] { tree });

            Assert.Equal("(;AP[Sharp SGF tools:0.0.1] RE[B+8.5] GM[1])", collection.ToString());
        }
    }
}