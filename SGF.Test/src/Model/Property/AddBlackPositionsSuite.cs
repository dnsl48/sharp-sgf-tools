using System.Collections.Generic;
using dnsl48.SGF.Model.Property.Setup;
using Xunit;

namespace dnsl48.SGF.Tests.Model.Property
{
    public class AddBlackPositionSuite
    {
        [Fact]
        public void TestSimple()
        {
            Assert.Equal("AB[aa]", new AddBlackPositions(new List<(byte x, byte y)>(new[] { ((byte)1, (byte)1) })).StringValue());
            Assert.Equal("AB[ab]", new AddBlackPositions(new List<(byte x, byte y)>(new[] { ((byte)1, (byte)2) })).StringValue());
            Assert.Equal("AB[bb]", new AddBlackPositions(new List<(byte x, byte y)>(new[] { ((byte)2, (byte)2) })).StringValue());
        }

        [Fact]
        public void TestLine()
        {
            Assert.Equal("AB[aa:ac]", new AddBlackPositions(new List<(byte x, byte y)>(new[] {
                ((byte)1, (byte)1),
                ((byte)1, (byte)2),
                ((byte)1, (byte)3)
            })).StringValue());
        }

        [Fact]
        public void TestAngle()
        {
            Assert.Equal("AB[aa:ab][bb]", new AddBlackPositions(new List<(byte x, byte y)>(new[] {
                ((byte)1, (byte)1),
                ((byte)1, (byte)2),
                ((byte)2, (byte)2)
            })).StringValue());
            Assert.Equal("AB[aa:cb][ac][cc][ee]", new AddBlackPositions(new List<(byte x, byte y)>(new[] {
                ((byte)1, (byte)1), ((byte)2, (byte)1), ((byte)3, (byte)1),
                ((byte)1, (byte)2), ((byte)1, (byte)3), ((byte)2, (byte)2),
                ((byte)3, (byte)2), ((byte)3, (byte)3), ((byte)5, (byte)5)
            })).StringValue());
        }

        [Fact]
        public void TestParse()
        {
            var positions = (AddBlackPositions) AddBlackPositions.Parse(new List<string>(new[] {
                "jd", "je", "jf", "jg", "kn:lq", "pn:pq"
            }));

            Assert.NotNull(positions);

            var hset = new HashSet<(byte x, byte y)>(positions.GetPositions());
            Assert.Contains((x: (byte)10, y: (byte)4), hset);
            Assert.Contains((x: (byte)10, y: (byte)5), hset);
            Assert.Contains((x: (byte)10, y: (byte)6), hset);
            Assert.Contains((x: (byte)10, y: (byte)7), hset);

            Assert.Contains((x: (byte)11, y: (byte)14), hset);
            Assert.Contains((x: (byte)11, y: (byte)15), hset);
            Assert.Contains((x: (byte)11, y: (byte)16), hset);
            Assert.Contains((x: (byte)11, y: (byte)17), hset);

            Assert.Contains((x: (byte)12, y: (byte)14), hset);
            Assert.Contains((x: (byte)12, y: (byte)15), hset);
            Assert.Contains((x: (byte)12, y: (byte)16), hset);
            Assert.Contains((x: (byte)12, y: (byte)17), hset);

            Assert.Contains((x: (byte)16, y: (byte)14), hset);
            Assert.Contains((x: (byte)16, y: (byte)15), hset);
            Assert.Contains((x: (byte)16, y: (byte)16), hset);
            Assert.Contains((x: (byte)16, y: (byte)17), hset);

            Assert.Equal(16, hset.Count);

            Assert.Equal("AB[jd:jg][kn:lq][pn:pq]", positions.StringValue());
        }
    }
}
