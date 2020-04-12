using dnsl48.SGF.Lexer;
using Pidgin;

using Xunit;

namespace dnsl48.SGF.Tests.Lexer
{
    public class NaiveLexerSuite
    {
        [Fact]
        public void PropertyValue()
        {
            Assert.Equal("test one", NaiveLexer.PropValue.Parse("[test one]").Value);
            Assert.Equal("test ] two", NaiveLexer.PropValue.Parse("[test \\] two] junk").Value);
            Assert.Equal("test three", NaiveLexer.PropValue.Parse("[test three] junk]").Value);
        }

        [Fact]
        public void PropertyIdent()
        {
            Assert.Equal("AA", NaiveLexer.PropIdent.Parse("AA[test one]").Value);
            Assert.Equal("BB", NaiveLexer.PropIdent.Parse(" BB[test two] ").Value);
            Assert.Equal("CC", NaiveLexer.PropIdent.Parse("  CC  [test three] ").Value);
        }

        [Fact]
        public void Property()
        {
            var p0 = NaiveLexer.PropertyParser.Parse("ZZ[]").Value;
            Assert.Equal("ZZ", p0.label);
            Assert.Single(p0.values);
            Assert.Contains("", p0.values);

            var p01 = NaiveLexer.PropertyParser.Parse("  ZZ[]  ").Value;
            Assert.Equal("ZZ", p01.label);
            Assert.Single(p01.values);
            Assert.Contains("", p01.values);

            var p02 = NaiveLexer.PropertyParser.Parse("  ZZ[] YY[] ").Value;
            Assert.Equal("ZZ", p02.label);
            Assert.Single(p02.values);
            Assert.Contains("", p01.values);

            var p1 = NaiveLexer.PropertyParser.Parse("AA[test one]").Value;
            Assert.Equal("AA", p1.label);
            Assert.Single(p1.values);
            Assert.Contains("test one", p1.values);

            var p2 = NaiveLexer.PropertyParser.Parse(" BB[ test \\] two ] junk ").Value;
            Assert.Equal("BB", p2.label);
            Assert.Single(p2.values);
            Assert.Contains(" test ] two ", p2.values);

            var p3 = NaiveLexer.PropertyParser.Parse("  CC  [test three] ").Value;
            Assert.Equal("CC", p3.label);
            Assert.Single(p3.values);
            Assert.Contains("test three", p3.values);

            var p4 = NaiveLexer.PropertyParser.Parse("DD[one][two][three]").Value;
            Assert.Equal("DD", p4.label);
            Assert.Equal(3, p4.values.Count);
            Assert.Contains("one", p4.values);
            Assert.Contains("two", p4.values);
            Assert.Contains("three", p4.values);

            var p5 = NaiveLexer.PropertyParser.Parse("EE [one] [two][three]").Value;
            Assert.Equal("EE", p5.label);
            Assert.Equal(3, p5.values.Count);
            Assert.Contains("one", p5.values);
            Assert.Contains("two", p5.values);
            Assert.Contains("three", p5.values);
        }

        [Fact]
        public void Node()
        {
            var n1 = NaiveLexer.NodeParser.Parse("AA[one]").Value;
            Assert.Single(n1.properties);
            var n1p0 = n1.properties[0];
            Assert.Equal("AA", n1p0.label);
            Assert.Single(n1p0.values);
            Assert.Contains("one", n1p0.values);

            var n2 = NaiveLexer.NodeParser.Parse("AA[one]BB[two]").Value;
            Assert.Equal(2, n2.properties.Count);
            Assert.Equal("AA", n2.properties[0].label);
            Assert.Equal("BB", n2.properties[1].label);
            Assert.Equal("one", n2.properties[0].values[0]);
            Assert.Equal("two", n2.properties[1].values[0]);

            var data = "  AAA[one] BB[one] [two]  CC [one] [two] [three] DD[] ; EE[] ";
            var nodes = NaiveLexer.NodeParser.Parse(data).Value;

            Assert.Equal(4, nodes.properties.Count);

            var p0 = nodes.properties[0];
            Assert.Equal("AAA", p0.label);
            Assert.Single(p0.values);
            Assert.Equal("one", p0.values[0]);

            var p1 = nodes.properties[1];
            Assert.Equal("BB", p1.label);
            Assert.Equal(2, p1.values.Count);
            Assert.Equal("one", p1.values[0]);
            Assert.Equal("two", p1.values[1]);

            var p2 = nodes.properties[2];
            Assert.Equal("CC", p2.label);
            Assert.Equal(3, p2.values.Count);
            Assert.Equal("one", p2.values[0]);
            Assert.Equal("two", p2.values[1]);
            Assert.Equal("three", p2.values[2]);

            var p3 = nodes.properties[3];
            Assert.Equal("DD", p3.label);
            Assert.Single(p3.values);
            Assert.Equal("", p3.values[0]);
        }

        [Fact]
        public void TestSequenceCount()
        {
            Assert.Single(NaiveLexer.SequenceParser.Parse("AA[] BB[]").Value.nodes);
            Assert.Equal(2, NaiveLexer.SequenceParser.Parse(" ; AA[] BB[]").Value.nodes.Count);
            Assert.Equal(3, NaiveLexer.SequenceParser.Parse(" ; AA[] BB[] ; ").Value.nodes.Count);
        }

        [Fact]
        public void TestSequenceData()
        {
            var data = "; AA[one] ; BB[one] CC [two] [three] ;;";
            var seq = NaiveLexer.SequenceParser.Parse(data).Value;

            Assert.Equal(5, seq.nodes.Count);

            var n0 = seq.nodes[0];
            Assert.Empty(n0.properties);

            var n1 = seq.nodes[1];
            Assert.Single(n1.properties);
            var n1p0 = n1.properties[0];
            Assert.Equal("AA", n1p0.label);
            Assert.Single(n1p0.values);
            Assert.Equal("one", n1p0.values[0]);

            var n2 = seq.nodes[2];
            Assert.Equal(2, n2.properties.Count);
            var n2p0 = n2.properties[0];
            Assert.Equal("BB", n2p0.label);
            Assert.Single(n2p0.values);
            Assert.Equal("one", n2p0.values[0]);
            var n2p1 = n2.properties[1];
            Assert.Equal("CC", n2p1.label);
            Assert.Equal(2, n2p1.values.Count);
            Assert.Equal("two", n2p1.values[0]);
            Assert.Equal("three", n2p1.values[1]);

            Assert.Empty(seq.nodes[3].properties);
            Assert.Empty(seq.nodes[4].properties);
        }

        [Fact]
        public void TestTreeSequence()
        {
            var data = @"(;
EV[36th Female Honinbo Title]
RO[3]
PB[Xie Yimin]
PW[Fujisawa Rina])";

            var t0 = NaiveLexer.TreeParser.Parse(data).Value;

            Assert.Empty(t0.children);
            Assert.Equal(2, t0.sequence.nodes.Count);
            Assert.Empty(t0.sequence.nodes[0].properties);

            var n0 = t0.sequence.nodes[1];

            Assert.Equal(4, n0.properties.Count);

            var p0 = n0.properties[0];
            Assert.Equal("EV", p0.label);
            Assert.Single(p0.values);
            Assert.Equal("36th Female Honinbo Title", p0.values[0]);

            var p1 = n0.properties[1];
            Assert.Equal("RO", p1.label);
            Assert.Single(p1.values);
            Assert.Equal("3", p1.values[0]);

            var p2 = n0.properties[2];
            Assert.Equal("PB", p2.label);
            Assert.Single(p2.values);
            Assert.Equal("Xie Yimin", p2.values[0]);

            var p3 = n0.properties[3];
            Assert.Equal("PW", p3.label);
            Assert.Single(p3.values);
            Assert.Equal("Fujisawa Rina", p3.values[0]);
        }

        [Fact]
        public void TestTreeRecursively()
        {
            var data = "(;PW[Lee Sedol]PB[AlphaGo](;W[qn]C[Through here, everything proceeded normally.])(;W[ed]C[AlphaGo anticipated something along the lines of this sequence.](;THRD[])))";

            var t0 = NaiveLexer.TreeParser.Parse(data).Value;
            Assert.Equal(2, t0.children.Count);
            Assert.Equal(2, t0.sequence.nodes.Count);

            var t0n0 = t0.sequence.nodes[0];
            Assert.Empty(t0n0.properties);

            var t0n1 = t0.sequence.nodes[1];
            Assert.Equal(2, t0n1.properties.Count);

            var t0n1p0 = t0n1.properties[0];
            Assert.Equal("PW", t0n1p0.label);
            Assert.Single(t0n1p0.values);
            Assert.Equal("Lee Sedol", t0n1p0.values[0]);

            var t0n1p1 = t0n1.properties[1];
            Assert.Equal("PB", t0n1p1.label);
            Assert.Single(t0n1p1.values);
            Assert.Equal("AlphaGo", t0n1p1.values[0]);

            var t00 = t0.children[0];
            Assert.Empty(t00.children);
            Assert.Equal(2, t00.sequence.nodes.Count);

            Assert.Empty(t00.sequence.nodes[0].properties);

            var t00n1 = t00.sequence.nodes[1];
            Assert.Equal(2, t00n1.properties.Count);

            var t00n1p0 = t00n1.properties[0];
            Assert.Equal("W", t00n1p0.label);
            Assert.Single(t00n1p0.values);
            Assert.Equal("qn", t00n1p0.values[0]);

            var t00n1p1 = t00n1.properties[1];
            Assert.Equal("C", t00n1p1.label);
            Assert.Single(t00n1p1.values);
            Assert.Equal("Through here, everything proceeded normally.", t00n1p1.values[0]);

            var t01 = t0.children[1];
            Assert.Single(t01.children);
            Assert.Equal(2, t01.sequence.nodes.Count);

            Assert.Empty(t01.sequence.nodes[0].properties);

            var t01n1 = t01.sequence.nodes[1];
            Assert.Equal(2, t00n1.properties.Count);

            var t01n1p0 = t01n1.properties[0];
            Assert.Equal("W", t01n1p0.label);
            Assert.Single(t01n1p0.values);
            Assert.Equal("ed", t01n1p0.values[0]);

            var t01n1p1 = t01n1.properties[1];
            Assert.Equal("C", t01n1p1.label);
            Assert.Single(t01n1p1.values);
            Assert.Equal("AlphaGo anticipated something along the lines of this sequence.", t01n1p1.values[0]);

            var t010 = t01.children[0];
            Assert.Empty(t010.children);
            Assert.Equal(2, t010.sequence.nodes.Count);
            Assert.Empty(t010.sequence.nodes[0].properties);
            Assert.Single(t010.sequence.nodes[1].properties);

            var t010p0 = t010.sequence.nodes[1].properties[0];
            Assert.Equal("THRD", t010p0.label);
            Assert.Single(t010p0.values);
            Assert.Equal("", t010p0.values[0]);
        }
    }
}
