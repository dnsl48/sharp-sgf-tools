using dnsl48.SGF.Model.Property.Annotation;
using dnsl48.SGF.Types;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class CommentSuite
    {
        [Fact]
        public void TestComment()
        {
            Assert.Equal("Comment content", new Comment(new Text("Comment content")).GetValue().source);
        }

        [Fact]
        public void TestParseCanHaveOnlyOneValue()
        {
            Assert.Null(Comment.Parse(new System.Collections.Generic.List<string>(new[] { "This is the comment", "Second value..." })));
            Assert.NotNull(Comment.Parse(new System.Collections.Generic.List<string>(new[] { "" })));
            Assert.Null(Comment.Parse(new System.Collections.Generic.List<string>()));
        }

        [Fact]
        public void TestParse()
        {
            var comment = (Comment) Comment.Parse(new System.Collections.Generic.List<string>(new[] { "This is the comment" }));
            Assert.NotNull(comment);
            Assert.Equal("This is the comment", comment.GetValue().source);
        }
    }
}
