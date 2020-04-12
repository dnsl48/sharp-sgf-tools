using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property.Annotation;
using dnsl48.SGF.Model.Property.Info;
using dnsl48.SGF.Model.Property.Markup;
using dnsl48.SGF.Model.Property.Misc;
using dnsl48.SGF.Model.Property.Move;
using dnsl48.SGF.Model.Property.Root;
using dnsl48.SGF.Model.Property.Setup;
using dnsl48.SGF.Model.Property.Timing;
using System.IO;

using Xunit;

namespace dnsl48.SGF.Tests.NaiveParser
{
    public class NaiveParserSuiteFF4
    {
        private TextReader _GetExampleSgf()
        {
            var stream = typeof(NaiveParserSuiteFF4).Assembly.GetManifestResourceStream("SGF.Test.res.ff4_example.sgf");
            return new StreamReader(stream);
        }

        private Collection _GetExampleCollection()
        {
            var txt = _GetExampleSgf().ReadToEnd();

            var parser = new dnsl48.SGF.NaiveParser();
            var collection = parser.Parse(_GetExampleSgf());

            return collection;
        }

        [Fact]
        public void TestEasyTsumego()
        {
            var content =
@"(
    ;
    GM[1]
    FF[4]
    SZ[19]

    ;
    AW[pb][qb][rb][rc][rd]
    AB[ob][nc][pc][qc][qe][re]
    PL[W]
    UC[1]

    (
        ;W[sa]TE[1]
        ;B[sd]
        (
            ;W[sc]TE[2]
            ;RE[W+R]
        )
    )
)";

            var parser = new dnsl48.SGF.NaiveParser();
            var collection = parser.Parse(content);

            Assert.Single(collection.trees);

            var tree = collection.trees[0];

            Assert.Equal(2, tree.nodes.Count);
            Assert.Single(tree.children);

            var node = tree.nodes[0];
            Assert.Equal("GM[1]", node.props["GM"].StringValue());
            Assert.Equal("FF[4]", node.props["FF"].StringValue());
            Assert.Equal("SZ[19]", node.props["SZ"].StringValue());

            node = tree.nodes[1];
            Assert.Equal("AW[pb:rb][rc:rd]", node.props["AW"].StringValue());
            Assert.Equal("AB[nc][ob][pc:qc][qe:re]", node.props["AB"].StringValue());
            Assert.Equal("PL[W]", node.props["PL"].StringValue());
            Assert.Equal("UC[1]", node.props["UC"].StringValue());

            tree = tree.children[0];
            Assert.Equal(2, tree.nodes.Count);
            Assert.Single(tree.children);

            node = tree.nodes[0];

            // Assert.Equal();
        }

        [Fact]
        public void TestStructure()
        {
            var collection = _GetExampleCollection();
            Assert.Equal(2, collection.trees.Count);

            Assert.Single(collection.trees[0].nodes);
            Assert.Equal(6, collection.trees[0].nodes[0].props.Count);

            Assert.Equal(5, collection.trees[0].children.Count);

            Assert.Empty(collection.trees[0].children[0].children);
            Assert.Equal(13, collection.trees[0].children[0].nodes.Count);

            Assert.Empty(collection.trees[0].children[1].children);
            Assert.Equal(4, collection.trees[0].children[1].nodes.Count);

            Assert.Empty(collection.trees[0].children[2].children);
            Assert.Equal(4, collection.trees[0].children[2].nodes.Count);

            Assert.Equal(6, collection.trees[0].children[3].children.Count);
            Assert.Single(collection.trees[0].children[3].nodes);

            Assert.Empty(collection.trees[0].children[4].children);
            Assert.Equal(21, collection.trees[0].children[4].nodes.Count);


            Assert.Equal(2, collection.trees[1].nodes.Count);
            Assert.Equal(3, collection.trees[1].children.Count);
        }

        [Fact]
        public void TestTree0()
        {
            var tree = _GetExampleCollection().trees[0];
            var nodes = tree.nodes;
            Assert.Single(nodes);

            var node = nodes[0];

            // ;FF[4]AP[Primiview:3.1]GM[1]SZ[19]GN[Gametree 1: properties]US[Arno Hollosi]

            Assert.Equal(6, node.props.Count);

            var ff = (FileFormat) node.props["FF"];
            Assert.Equal(4, ff.GetValue());

            var ap = (Application) node.props["AP"];
            Assert.Equal("Primiview", ap.app.source);
            Assert.Equal("3.1", ap.version.source);

            var gm = (GameType) node.props["GM"];
            Assert.Equal(1, gm.GetValue());

            var sz = (BoardSize) node.props["SZ"];
            Assert.Equal(19, sz.x);
            Assert.Equal(19, sz.y);

            var gn = (GameName) node.props["GN"];
            Assert.Equal("Gametree 1: properties", gn.GetValue().source);

            var us = (GameProvider) node.props["US"];
            Assert.Equal("Arno Hollosi", us.GetValue().source);
        }

        [Fact]
        public void TestTree0Child0()
        {
//(;B[pd]N[Moves, comments, annotations]
// C[Nodename set to: "Moves, comments, annotations"];W[dp]GW[1]
// C[Marked as "Good for White"];B[pp]GB[2]
// C[Marked as "Very good for Black"];W[dc]GW[2]
// C[Marked as "Very good for White"];B[pj]DM[1]
// C[Marked as "Even position"];W[ci]UC[1]
// C[Marked as "Unclear position"];B[jd]TE[1]
// C[Marked as "Tesuji" or "Good move"];W[jp]BM[2]
// C[Marked as "Very bad move"];B[gd]DO[]
// C[Marked as "Doubtful move"];W[de]IT[]
// C[Marked as "Interesting move"];B[jj];
// C[White "Pass" move]W[];
// C[Black "Pass" move]B[tt])

            var nodes = _GetExampleCollection().trees[0].children[0].nodes;


            var node = nodes[0];
            Assert.Equal(3, node.props.Count);

            var c = (Comment) node.props["C"];
            Assert.Equal("Nodename set to: \"Moves, comments, annotations\"", c.GetValue().source);

            var b = (BlackMove) node.props["B"];
            Assert.Equal("pd", b.GetValue());

            var n = (NodeName) node.props["N"];
            Assert.Equal("Moves, comments, annotations", n.GetValue().source);


            node = nodes[1];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Good for White\"", c.GetValue().source);

            var w = (WhiteMove) node.props["W"];
            Assert.Equal("dp", w.GetValue());

            var gw = (GoodForWhite) node.props["GW"];
            Assert.Equal(1, gw.GetValue());


            node = nodes[2];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Very good for Black\"", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("pp", b.GetValue());

            var gb = (GoodForBlack) node.props["GB"];
            Assert.Equal(2, gb.GetValue());


            // ;W[dc]GW[2]C[Marked as "Very good for White"];
            node = nodes[3];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Very good for White\"", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("dc", w.GetValue());

            gw = (GoodForWhite) node.props["GW"];
            Assert.Equal(2, gw.GetValue());


            // ;B[pj]DM[1]C[Marked as "Even position"]
            node = nodes[4];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Even position\"", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("pj", b.GetValue());

            var dm = (EvenPosition) node.props["DM"];
            Assert.Equal(1, dm.GetValue());


            // ;W[ci]UC[1]C[Marked as "Unclear position"]
            node = nodes[5];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Unclear position\"", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("ci", w.GetValue());

            var uc = (UnclearPosition) node.props["UC"];
            Assert.Equal(1, uc.GetValue());


            // ;B[jd]TE[1]C[Marked as "Tesuji" or "Good move"]
            node = nodes[6];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Tesuji\" or \"Good move\"", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("jd", b.GetValue());

            var te = (GoodMove) node.props["TE"];
            Assert.Equal(1, te.GetValue());


            // ;W[jp]BM[2]C[Marked as "Very bad move"]
            node = nodes[7];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Very bad move\"", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("jp", w.GetValue());

            var bm = (BadMove) node.props["BM"];
            Assert.Equal(2, bm.GetValue());


            // ;B[gd]DO[]C[Marked as "Doubtful move"]
            node = nodes[8];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Doubtful move\"", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("gd", b.GetValue());

            var do_ = (DoubtfulMove) node.props["DO"];
            Assert.NotNull(do_);


            // ;W[de]IT[]C[Marked as "Interesting move"]
            node = nodes[9];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Marked as \"Interesting move\"", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("de", w.GetValue());

            var it = (InterestingMove) node.props["IT"];
            Assert.NotNull(it);


            // ;B[jj]
            node = nodes[10];
            Assert.Single(node.props);

            b = (BlackMove) node.props["B"];
            Assert.Equal("jj", b.GetValue());


            // ;C[White "Pass" move]W[]
            node = nodes[11];
            Assert.Equal(2, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("White \"Pass\" move", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("", w.GetValue());


            // ;C[Black "Pass" move]B[tt]
            node = nodes[12];
            Assert.Equal(2, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Black \"Pass\" move", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("tt", b.GetValue());
        }

        [Fact]
        public void TestTree0Child1()
        {
// (;AB[dd][de][df][dg][do:gq]
//   AW[jd][je][jf][jg][kn:lq][pn:pq]
// N[Setup]C[Black & white stones at the top are added as single stones.

// Black & white stones at the bottom are added using compressed point lists.]


// ;AE[ep][fp][kn][lo][lq][pn:pq]
// C[AddEmpty

// Black stones & stones of left white group are erased in FF[3\] way.

// White stones at bottom right were erased using compressed point list.]


// ;AB[pd]AW[pp]PL[B]C[Added two stones.

// Node marked with "Black to play".]

// ;PL[W]
// C[Node marked with "White to play"])


            var nodes = _GetExampleCollection().trees[0].children[1].nodes;


            // ;N[Setup]
            //  AB[dd][de][df][dg][do:gq]
            //  AW[jd][je][jf][jg][kn:lq][pn:pq]
            //  C[Black & white stones at the top are added as single stones.
            //    Black & white stones at the bottom are added using compressed point lists.]
            var node = nodes[0];
            Assert.Equal(4, node.props.Count);

            var n = (NodeName) node.props["N"];
            Assert.Equal("Setup", n.GetValue().source);

            var c = (Comment) node.props["C"];
            Assert.Equal("Black & white stones at the top are added as single stones.\n\nBlack & white stones at the bottom are added using compressed point lists.", c.GetValue().source);

            var ab = (AddBlackPositions) node.props["AB"];
            Assert.Equal("AB[dd:dg][do:gq]", ab.StringValue());

            var aw = (AddWhitePositions) node.props["AW"];
            Assert.Equal("AW[jd:jg][kn:lq][pn:pq]", aw.StringValue());


            // ;AE[ep][fp][kn][lo][lq][pn:pq]
            // C[AddEmpty
            // Black stones & stones of left white group are erased in FF[3\] way.
            // White stones at bottom right were erased using compressed point list.]

            node = nodes[1];
            Assert.Equal(2, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("AddEmpty\n\nBlack stones & stones of left white group are erased in FF[3] way.\n\nWhite stones at bottom right were erased using compressed point list.", c.GetValue().source);

            var ae = (ClearPositions) node.props["AE"];
            Assert.Equal("AE[ep:fp][kn][lo][lq][pn:pq]", ae.StringValue());


            // ;AB[pd]
            //  AW[pp]
            //  PL[B]
            //  C[Added two stones.
            //    Node marked with "Black to play".]

            node = nodes[2];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Added two stones.\n\nNode marked with \"Black to play\".", c.GetValue().source);

            ab = (AddBlackPositions) node.props["AB"];
            Assert.Equal("AB[pd]", ab.StringValue());

            aw = (AddWhitePositions) node.props["AW"];
            Assert.Equal("AW[pp]", aw.StringValue());

            var pl = (PlayerTurn) node.props["PL"];
            Assert.Equal(Colour.Black, pl.GetColour());


            // ;PL[W]
            // C[Node marked with "White to play"])

            node = nodes[3];
            Assert.Equal(2, node.props.Count);

            pl = (PlayerTurn) node.props["PL"];
            Assert.Equal(Colour.White, pl.GetColour());

            c = (Comment) node.props["C"];
            Assert.Equal("Node marked with \"White to play\"", c.GetValue().source);
        }

        [Fact]
        public void TestTree0Child2()
        {
// (;AB[dd][de][df][dg][dh][di][dj][nj][ni][nh][nf][ne][nd][ij][ii][ih][hq]
// [gq][fq][eq][dr][ds][dq][dp][cp][bp][ap][iq][ir][is][bo][bn][an][ms][mr]
// AW[pd][pe][pf][pg][ph][pi][pj][fd][fe][ff][fh][fi][fj][kh][ki][kj][os][or]
// [oq][op][pp][qp][rp][sp][ro][rn][sn][nq][mq][lq][kq][kr][ks][fs][gs][gr]
// [er]N[Markup]C[Position set up without compressed point lists.]

// ;TR[dd][de][df][ed][ee][ef][fd:ff]
//  MA[dh][di][dj][ej][ei][eh][fh:fj]
//  CR[nd][ne][nf][od][oe][of][pd:pf]
//  SQ[nh][ni][nj][oh][oi][oj][ph:pj]
//  SL[ih][ii][ij][jj][ji][jh][kh:kj]
//  TW[pq:ss][so][lr:ns]
//  TB[aq:cs][er:hs][ao]
// C[Markup at top partially using compressed point lists (for markup on white stones); listed clockwise, starting at upper left:
// - TR (triangle)
// - CR (circle)
// - SQ (square)
// - SL (selected points)
// - MA ('X')

// Markup at bottom: black & white territory (using compressed point lists)]
// ;LB[dc:1][fc:2][nc:3][pc:4][dj:a][fj:b][nj:c]
// [pj:d][gs:ABCDEFGH][gr:ABCDEFG][gq:ABCDEF][gp:ABCDE][go:ABCD][gn:ABC][gm:AB]
// [mm:12][mn:123][mo:1234][mp:12345][mq:123456][mr:1234567][ms:12345678]
// C[Label (LB property)

// Top: 8 single char labels (1-4, a-d)

// Bottom: Labels up to 8 char length.]

// ;DD[kq:os][dq:hs]
// AR[aa:sc][sa:ac][aa:sa][aa:ac][cd:cj]
//   [gd:md][fh:ij][kj:nh]
// LN[pj:pd][nf:ff][ih:fj][kh:nj]
// C[Arrows, lines and dimmed points.])

            var nodes = _GetExampleCollection().trees[0].children[2].nodes;


            // ;AB[dd][de][df][dg][dh][di][dj][nj][ni][nh][nf][ne][nd][ij][ii][ih][hq]
            //    [gq][fq][eq][dr][ds][dq][dp][cp][bp][ap][iq][ir][is][bo][bn][an][ms][mr]
            // AW[pd][pe][pf][pg][ph][pi][pj][fd][fe][ff][fh][fi][fj][kh][ki][kj][os][or]
            //   [oq][op][pp][qp][rp][sp][ro][rn][sn][nq][mq][lq][kq][kr][ks][fs][gs][gr][er]
            // N[Markup]
            // C[Position set up without compressed point lists.]
            var node = nodes[0];
            Assert.Equal(4, node.props.Count);

            var n = (NodeName) node.props["N"];
            Assert.Equal("Markup", n.GetValue().source);

            var c = (Comment) node.props["C"];
            Assert.Equal("Position set up without compressed point lists.", c.GetValue().source);

            var ab = (AddBlackPositions) node.props["AB"];
            Assert.Equal("AB[an:bn][ap:dp][bo][dd:dj][dq:iq][dr:ds][ih:ij][ir:is][mr:ms][nd:nf][nh:nj]", ab.StringValue());

            var aw = (AddWhitePositions) node.props["AW"];
            Assert.Equal("AW[er][fd:ff][fh:fj][fs:gs][gr][kh:kj][kq:oq][kr:ks][op:sp][or:os][pd:pj][rn:sn][ro]", aw.StringValue());


            // ;TR[dd][de][df][ed][ee][ef][fd:ff]
            //  MA[dh][di][dj][ej][ei][eh][fh:fj]
            //  CR[nd][ne][nf][od][oe][of][pd:pf]
            //  SQ[nh][ni][nj][oh][oi][oj][ph:pj]
            //  SL[ih][ii][ij][jj][ji][jh][kh:kj]
            //  TW[pq:ss][so][lr:ns]
            //  TB[aq:cs][er:hs][ao]
            // C[Markup at top partially using compressed point lists (for markup on white stones); listed clockwise, starting at upper left:
            // - TR (triangle)
            // - CR (circle)
            // - SQ (square)
            // - SL (selected points)
            // - MA ('X')
            // Markup at bottom: black & white territory (using compressed point lists)]

            node = nodes[1];
            Assert.Equal(8, node.props.Count);

            var tr = (TriangleMarker) node.props["TR"];
            Assert.Equal("TR[dd:ff]", tr.StringValue());

            var ma = (XMarker) node.props["MA"];
            Assert.Equal("MA[dh:fj]", ma.StringValue());

            var cr = (CircleMarker) node.props["CR"];
            Assert.Equal("CR[nd:pf]", cr.StringValue());

            var sq = (SquareMarker) node.props["SQ"];
            Assert.Equal("SQ[nh:pj]", sq.StringValue());

            var sl = (SelectedMarker) node.props["SL"];
            Assert.Equal("SL[ih:kj]", sl.StringValue());

            var tw = (TerritoryWhite) node.props["TW"];
            Assert.Equal("TW[lr:ns][pq:ss][so]", tw.StringValue());

            var tb = (TerritoryBlack) node.props["TB"];
            Assert.Equal("TB[ao][aq:cs][er:hs]", tb.StringValue());

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Markup at top partially using compressed point lists (for markup on white stones); listed clockwise, starting at upper left:
- TR (triangle)
- CR (circle)
- SQ (square)
- SL (selected points)
- MA ('X')

Markup at bottom: black & white territory (using compressed point lists)",
                c.GetValue().source
            );



            // ;LB[dc:1][fc:2][nc:3][pc:4][dj:a][fj:b][nj:c]
            //    [pj:d][gs:ABCDEFGH][gr:ABCDEFG][gq:ABCDEF][gp:ABCDE][go:ABCD][gn:ABC][gm:AB]
            //    [mm:12][mn:123][mo:1234][mp:12345][mq:123456][mr:1234567][ms:12345678]
            //  C[Label (LB property)
            // Top: 8 single char labels (1-4, a-d)
            // Bottom: Labels up to 8 char length.]

            node = nodes[2];
            Assert.Equal(2, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Label (LB property)

Top: 8 single char labels (1-4, a-d)

Bottom: Labels up to 8 char length.",
                c.GetValue().source
            );

            var lb = (TextLabel) node.props["LB"];
            Assert.Equal(22, lb.points.Count);
            Assert.Equal("LB[dc:1][dj:a][fc:2][fj:b][gm:AB][gn:ABC][go:ABCD][gp:ABCDE][gq:ABCDEF][gr:ABCDEFG][gs:ABCDEFGH][mm:12][mn:123][mo:1234][mp:12345][mq:123456][mr:1234567][ms:12345678][nc:3][nj:c][pc:4][pj:d]", lb.StringValue());



            // ;DD[kq:os][dq:hs]
            // AR[aa:sc][sa:ac][aa:sa][aa:ac][cd:cj]
            //   [gd:md][fh:ij][kj:nh]
            // LN[pj:pd][nf:ff][ih:fj][kh:nj]
            // C[Arrows, lines and dimmed points.])

            node = nodes[3];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Arrows, lines and dimmed points.", c.GetValue().source);

            var dd = (DimMarker) node.props["DD"];
            Assert.Equal("DD[dq:hs][kq:os]", dd.StringValue());

            var ar = (ArrowMarker) node.props["AR"];
            Assert.Equal("AR[aa:ac][aa:sa][aa:sc][cd:cj][fh:ij][gd:md][kj:nh][sa:ac]", ar.StringValue());

            var ln = (LineMarker) node.props["LN"];
            Assert.Equal("LN[ih:fj][kh:nj][nf:ff][pj:pd]", ln.StringValue());
        }

        [Fact]
        public void TestTree0Child3()
        {
// (;B[qd]N[Style & text type]
// C[There are hard linebreaks & soft linebreaks.
// Soft linebreaks are linebreaks preceeded by '\\' like this one >o\
// k<. Hard line breaks are all other linebreaks.
// Soft linebreaks are converted to >nothing<, i.e. removed.

// Note that linebreaks are coded differently on different systems.

// Examples (>ok< shouldn't be split):

// linebreak 1 "\\n": >o\
// k<
// linebreak 2 "\\n\\r": >o\

// k<
// linebreak 3 "\\r\\n": >o\
// k<
// linebreak 4 "\\r": >o\
// k<]

// (;W[dd]N[W d16]C[Variation C is better.](;B[pp]N[B q4])
// (;B[dp]N[B d4])
// (;B[pq]N[B q3])
// (;B[oq]N[B p3])
// )
// (;W[dp]N[W d4])
// (;W[pp]N[W q4])
// (;W[cc]N[W c17])
// (;W[cq]N[W c3])
// (;W[qq]N[W r3])
// )

            var tree = _GetExampleCollection().trees[0].children[3];
            var nodes = tree.nodes;


            // ;B[qd]
            // N[Style & text type]
            // C[There are hard linebreaks & soft linebreaks.
            // Soft linebreaks are linebreaks preceeded by '\\' like this one >o\
            // k<. Hard line breaks are all other linebreaks.
            // Soft linebreaks are converted to >nothing<, i.e. removed.

            // Note that linebreaks are coded differently on different systems.

            // Examples (>ok< shouldn't be split):

            // linebreak 1 "\\n": >o\
            // k<
            // linebreak 2 "\\n\\r": >o\

            // k<
            // linebreak 3 "\\r\\n": >o\
            // k<
            // linebreak 4 "\\r": >o\
            // k<]
            // var node = nodes[0];
            // Assert.Equal(3, node.props.Count);

            var node = nodes[0];
            Assert.Equal(3, node.props.Count);

            var b = (BlackMove) node.props["B"];
            Assert.Equal("qd", b.GetValue());

            var n = (NodeName) node.props["N"];
            Assert.Equal("Style & text type", n.GetValue().source);

            var c = (Comment) node.props["C"];
            Assert.Equal(
@"There are hard linebreaks & soft linebreaks.
Soft linebreaks are linebreaks preceeded by '\' like this one >ok<. Hard line breaks are all other linebreaks.
Soft linebreaks are converted to >nothing<, i.e. removed.

Note that linebreaks are coded differently on different systems.

Examples (>ok< shouldn't be split):

linebreak 1 ""\n"": >ok<
linebreak 2 ""\n\r"": >ok<
linebreak 3 ""\r\n"": >ok<
linebreak 4 ""\r"": >ok<",
                c.GetValue().source
            );

            // (;W[dd]N[W d16]C[Variation C is better.](;B[pp]N[B q4])
            // (;B[dp]N[B d4])
            // (;B[pq]N[B q3])
            // (;B[oq]N[B p3])
            // )

            var child = tree.children[0];

            Assert.Single(child.nodes);
            Assert.Equal(4, child.children.Count);

            node = child.nodes[0];
            Assert.Equal(3, node.props.Count);

            var w = (WhiteMove) node.props["W"];
            Assert.Equal("dd", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W d16", n.GetValue().source);

            c = (Comment) node.props["C"];
            Assert.Equal("Variation C is better.", c.GetValue().source);

            // (;B[pp]N[B q4])
            var cc = child.children[0];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("pp", b.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("B q4", n.GetValue().source);


            // (;B[dp]N[B d4])
            cc = child.children[1];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("dp", b.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("B d4", n.GetValue().source);


            // (;B[pq]N[B q3])
            cc = child.children[2];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("pq", b.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("B q3", n.GetValue().source);


            // (;B[oq]N[B p3])
            cc = child.children[3];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("oq", b.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("B p3", n.GetValue().source);


            // (;W[dp]N[W d4])
            child = tree.children[1];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);
            node = child.nodes[0];
            Assert.Equal(2, node.props.Count);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("dp", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W d4", n.GetValue().source);


            // (;W[pp]N[W q4])
            child = tree.children[2];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);
            node = child.nodes[0];
            Assert.Equal(2, node.props.Count);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("pp", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W q4", n.GetValue().source);


            // (;W[cc]N[W c17])
            child = tree.children[3];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);
            node = child.nodes[0];
            Assert.Equal(2, node.props.Count);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("cc", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W c17", n.GetValue().source);


            // (;W[cq]N[W c3])
            child = tree.children[4];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);
            node = child.nodes[0];
            Assert.Equal(2, node.props.Count);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("cq", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W c3", n.GetValue().source);


            // (;W[qq]N[W r3])
            child = tree.children[5];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);
            node = child.nodes[0];
            Assert.Equal(2, node.props.Count);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("qq", w.GetValue());

            n = (NodeName) node.props["N"];
            Assert.Equal("W r3", n.GetValue().source);
        }

        [Fact]
        public void TestTree0Child4()
        {
// (;B[qr]N[Time limits, captures & move numbers]
// BL[120.0]C[Black time left: 120 sec];W[rr]
// WL[300]C[White time left: 300 sec];B[rq]
// BL[105.6]OB[10]C[Black time left: 105.6 sec
// Black stones left (in this byo-yomi period): 10];W[qq]
// WL[200]OW[2]C[White time left: 200 sec
// White stones left: 2];B[sr]
// BL[87.00]OB[9]C[Black time left: 87 sec
// Black stones left: 9];W[qs]
// WL[13.20]OW[1]C[White time left: 13.2 sec
// White stones left: 1];B[rs]
// C[One white stone at s2 captured];W[ps];B[pr];W[or]
// MN[2]C[Set move number to 2];B[os]
// C[Two white stones captured
// (at q1 & r1)]
// ;MN[112]W[pq]C[Set move number to 112];B[sq];W[rp];B[ps]
// ;W[ns];B[ss];W[nr]
// ;B[rr];W[sp];B[qs]C[Suicide move
// (all B stones get captured)])

            var nodes = _GetExampleCollection().trees[0].children[4].nodes;


            // ;B[qr]
            //  N[Time limits, captures & move numbers]
            //  BL[120.0]
            //  C[Black time left: 120 sec]
            var node = nodes[0];
            Assert.Equal(4, node.props.Count);

            var c = (Comment) node.props["C"];
            Assert.Equal("Black time left: 120 sec", c.GetValue().source);

            var n = (NodeName) node.props["N"];
            Assert.Equal("Time limits, captures & move numbers", n.GetValue().source);

            var b = (BlackMove) node.props["B"];
            Assert.Equal("qr", b.GetValue());

            var bl = (TimeLeftBlack) node.props["BL"];
            Assert.Equal(120m, bl.GetValue());


            // ;W[rr]
            //  WL[300]
            //  C[White time left: 300 sec]
            node = nodes[1];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("White time left: 300 sec", c.GetValue().source);

            var w = (WhiteMove) node.props["W"];
            Assert.Equal("rr", w.GetValue());

            var wl = (TimeLeftWhite) node.props["WL"];
            Assert.Equal(300m, wl.GetValue());


            // ;B[rq]
            //  BL[105.6]
            //  OB[10]
            //  C[Black time left: 105.6 sec
            //    Black stones left (in this byo-yomi period): 10]
            node = nodes[2];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Black time left: 105.6 sec\nBlack stones left (in this byo-yomi period): 10", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("rq", b.GetValue());

            bl = (TimeLeftBlack) node.props["BL"];
            Assert.Equal(105.6m, bl.GetValue());

            var ob = (MovesLeftBlack) node.props["OB"];
            Assert.Equal(10, ob.GetValue());


            // ;W[qq]
            //  WL[200]
            //  OW[2]
            //  C[White time left: 200 sec
            //    White stones left: 2]
            node = nodes[3];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("White time left: 200 sec\nWhite stones left: 2", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("qq", w.GetValue());

            wl = (TimeLeftWhite) node.props["WL"];
            Assert.Equal(200m, wl.GetValue());

            var ow = (MovesLeftWhite) node.props["OW"];
            Assert.Equal(2, ow.GetValue());


            // ;B[sr]
            //  BL[87.00]
            //  OB[9]
            //  C[Black time left: 87 sec
            //    Black stones left: 9]
            node = nodes[4];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Black time left: 87 sec\nBlack stones left: 9", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("sr", b.GetValue());

            bl = (TimeLeftBlack) node.props["BL"];
            Assert.Equal(87m, bl.GetValue());

            ob = (MovesLeftBlack) node.props["OB"];
            Assert.Equal(9, ob.GetValue());


            // ;W[qs]
            //  WL[13.20]
            //  OW[1]
            //  C[White time left: 13.2 sec
            //    White stones left: 1]
            node = nodes[5];
            Assert.Equal(4, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("White time left: 13.2 sec\nWhite stones left: 1", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("qs", w.GetValue());

            wl = (TimeLeftWhite) node.props["WL"];
            Assert.Equal(13.2m, wl.GetValue());

            ow = (MovesLeftWhite) node.props["OW"];
            Assert.Equal(1, ow.GetValue());


            // ;B[rs]
            // C[One white stone at s2 captured];
            node = nodes[6];
            Assert.Equal(2, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("One white stone at s2 captured", c.GetValue().source);

            b = (BlackMove) node.props["B"];
            Assert.Equal("rs", b.GetValue());


            // ;W[ps];B[pr]
            node = nodes[7];
            Assert.Single(node.props);
            w = (WhiteMove) node.props["W"];
            Assert.Equal("ps", w.GetValue());
            node = nodes[8];
            Assert.Single(node.props);
            b = (BlackMove) node.props["B"];
            Assert.Equal("pr", b.GetValue());


            // ;W[or]
            //  MN[2]
            //  C[Set move number to 2]
            node = nodes[9];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Set move number to 2", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("or", w.GetValue());

            var mn = (MoveNumber) node.props["MN"];
            Assert.Equal(2, mn.GetValue());


            // ;B[os]
            // C[Two white stones captured
            //   (at q1 & r1)]
            node = nodes[10];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("os", b.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal("Two white stones captured\n(at q1 & r1)", c.GetValue().source);


            // ;MN[112]W[pq]C[Set move number to 112]
            node = nodes[11];
            Assert.Equal(3, node.props.Count);

            c = (Comment) node.props["C"];
            Assert.Equal("Set move number to 112", c.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("pq", w.GetValue());

            mn = (MoveNumber) node.props["MN"];
            Assert.Equal(112, mn.GetValue());


            // ;B[sq];W[rp];B[ps];W[ns];B[ss];W[nr];B[rr];W[sp]
            node = nodes[12];
            Assert.Single(node.props);
            b = (BlackMove) node.props["B"];
            Assert.Equal("sq", b.GetValue());

            node = nodes[13];
            Assert.Single(node.props);
            w = (WhiteMove) node.props["W"];
            Assert.Equal("rp", w.GetValue());

            node = nodes[14];
            Assert.Single(node.props);
            b = (BlackMove) node.props["B"];
            Assert.Equal("ps", b.GetValue());

            node = nodes[15];
            Assert.Single(node.props);
            w = (WhiteMove) node.props["W"];
            Assert.Equal("ns", w.GetValue());

            node = nodes[16];
            Assert.Single(node.props);
            b = (BlackMove) node.props["B"];
            Assert.Equal("ss", b.GetValue());

            node = nodes[17];
            Assert.Single(node.props);
            w = (WhiteMove) node.props["W"];
            Assert.Equal("nr", w.GetValue());

            node = nodes[18];
            Assert.Single(node.props);
            b = (BlackMove) node.props["B"];
            Assert.Equal("rr", b.GetValue());

            node = nodes[19];
            Assert.Single(node.props);
            w = (WhiteMove) node.props["W"];
            Assert.Equal("sp", w.GetValue());


            // ;B[qs]C[Suicide move\n(all B stones get captured)]
            node = nodes[20];
            Assert.Equal(2, node.props.Count);

            b = (BlackMove) node.props["B"];
            Assert.Equal("qs", b.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal("Suicide move\n(all B stones get captured)", c.GetValue().source);
        }

        [Fact]
        public void TestTree1()
        {
            var tree = _GetExampleCollection().trees[1];
            var nodes = tree.nodes;
            Assert.Equal(2, nodes.Count);


            // ;FF[4]
            //  AP[Primiview:3.1]
            //  GM[1]
            //  SZ[19]
            //  C[Gametree 2: game-info
            // Game-info properties are usually stored in the root node.
            // If games are merged into a single game-tree, they are stored in the node\
            //  where the game first becomes distinguishable from all other games in\
            //  the tree.]
            var node = nodes[0];
            Assert.Equal(5, node.props.Count);

            var ff = (FileFormat) node.props["FF"];
            Assert.Equal(4, ff.GetValue());

            var ap = (Application) node.props["AP"];
            Assert.Equal("Primiview", ap.app.source);
            Assert.Equal("3.1", ap.version.source);

            var gm = (GameType) node.props["GM"];
            Assert.Equal(1, gm.GetValue());

            var sz = (BoardSize) node.props["SZ"];
            Assert.Equal(19, sz.x);
            Assert.Equal(19, sz.y);

            var c = (Comment) node.props["C"];
            Assert.Equal(
@"Gametree 2: game-info

Game-info properties are usually stored in the root node.
If games are merged into a single game-tree, they are stored in the node where the game first becomes distinguishable from all other games in the tree.",
                c.GetValue().source
            );


            // ;B[pd]
            node = nodes[1];
            Assert.Single(node.props);

            var b = (BlackMove) node.props["B"];
            Assert.Equal("pd", b.GetValue());


// (;PW[W. Hite]WR[6d]RO[2]RE[W+3.5]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[dp]
// C[Game-info:
// Black: B. Lack, 5d
// White: W. Hite, 6d
// Place: London
// Event: Go Congress
// Round: 2
// Result: White wins by 3.5])
            var child = tree.children[0];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);

            node = child.nodes[0];

            var pw = (WhitePlayerName) node.props["PW"];
            Assert.Equal("W. Hite", pw.GetValue().source);

            var wr = (WhitePlayerRank) node.props["WR"];
            Assert.Equal("6d", wr.GetValue().source);

            var ro = (RoundNumber) node.props["RO"];
            Assert.Equal("2", ro.GetValue().source);

            var re = (GameResult) node.props["RE"];
            Assert.Equal(GameResult.WinType.Score, re.how);
            Assert.Equal(Colour.White, re.who);
            Assert.Equal(3.5m, re.score);

            var pb = (BlackPlayerName) node.props["PB"];
            Assert.Equal("B. Lack", pb.GetValue().source);

            var br = (BlackPlayerRank) node.props["BR"];
            Assert.Equal("5d", br.GetValue().source);

            var pc = (Venue) node.props["PC"];
            Assert.Equal("London", pc.GetValue().source);

            var ev = (EventName) node.props["EV"];
            Assert.Equal("Go Congress", ev.GetValue().source);

            var w = (WhiteMove) node.props["W"];
            Assert.Equal("dp", w.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Game-info:
Black: B. Lack, 5d
White: W. Hite, 6d
Place: London
Event: Go Congress
Round: 2
Result: White wins by 3.5",
                c.GetValue().source
            );



// (;PW[T. Suji]WR[7d]RO[1]RE[W+Resign]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[cp]
// C[Game-info:
// Black: B. Lack, 5d
// White: T. Suji, 7d
// Place: London
// Event: Go Congress
// Round: 1
// Result: White wins by resignation])
            child = tree.children[1];
            Assert.Empty(child.children);
            Assert.Single(child.nodes);

            node = child.nodes[0];

            pw = (WhitePlayerName) node.props["PW"];
            Assert.Equal("T. Suji", pw.GetValue().source);

            wr = (WhitePlayerRank) node.props["WR"];
            Assert.Equal("7d", wr.GetValue().source);

            ro = (RoundNumber) node.props["RO"];
            Assert.Equal("1", ro.GetValue().source);

            re = (GameResult) node.props["RE"];
            Assert.Equal(GameResult.WinType.Resign, re.how);
            Assert.Equal(Colour.White, re.who);
            Assert.Equal(0m, re.score);

            pb = (BlackPlayerName) node.props["PB"];
            Assert.Equal("B. Lack", pb.GetValue().source);

            br = (BlackPlayerRank) node.props["BR"];
            Assert.Equal("5d", br.GetValue().source);

            pc = (Venue) node.props["PC"];
            Assert.Equal("London", pc.GetValue().source);

            ev = (EventName) node.props["EV"];
            Assert.Equal("Go Congress", ev.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("cp", w.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Game-info:
Black: B. Lack, 5d
White: T. Suji, 7d
Place: London
Event: Go Congress
Round: 1
Result: White wins by resignation",
                c.GetValue().source
            );



// (;W[ep];B[pp]
// (;PW[S. Abaki]WR[1d]RO[3]RE[B+63.5]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[ed]
// C[Game-info:
// Black: B. Lack, 5d
// White: S. Abaki, 1d
// Place: London
// Event: Go Congress
// Round: 3
// Result: Balck wins by 63.5])
// (;PW[A. Tari]WR[12k]KM[-59.5]RO[4]RE[B+R]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[cd]
// C[Game-info:
// Black: B. Lack, 5d
// White: A. Tari, 12k
// Place: London
// Event: Go Congress
// Round: 4
// Komi: -59.5 points
// Result: Black wins by resignation])
// )

            child = tree.children[2];
            Assert.Equal(2, child.children.Count);
            Assert.Equal(2, child.nodes.Count);

            node = child.nodes[0];
            Assert.Single(node.props);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("ep", w.GetValue());

            node = child.nodes[1];
            Assert.Single(node.props);

            b = (BlackMove) node.props["B"];
            Assert.Equal("pp", b.GetValue());



// (;PW[S. Abaki]WR[1d]RO[3]RE[B+63.5]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[ed]
// C[Game-info:
// Black: B. Lack, 5d
// White: S. Abaki, 1d
// Place: London
// Event: Go Congress
// Round: 3
// Result: Balck wins by 63.5])

            var cc = child.children[0];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];

            pw = (WhitePlayerName) node.props["PW"];
            Assert.Equal("S. Abaki", pw.GetValue().source);

            wr = (WhitePlayerRank) node.props["WR"];
            Assert.Equal("1d", wr.GetValue().source);

            ro = (RoundNumber) node.props["RO"];
            Assert.Equal("3", ro.GetValue().source);

            re = (GameResult) node.props["RE"];
            Assert.Equal(GameResult.WinType.Score, re.how);
            Assert.Equal(Colour.Black, re.who);
            Assert.Equal(63.5m, re.score);

            pb = (BlackPlayerName) node.props["PB"];
            Assert.Equal("B. Lack", pb.GetValue().source);

            br = (BlackPlayerRank) node.props["BR"];
            Assert.Equal("5d", br.GetValue().source);

            pc = (Venue) node.props["PC"];
            Assert.Equal("London", pc.GetValue().source);

            ev = (EventName) node.props["EV"];
            Assert.Equal("Go Congress", ev.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("ed", w.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Game-info:
Black: B. Lack, 5d
White: S. Abaki, 1d
Place: London
Event: Go Congress
Round: 3
Result: Balck wins by 63.5",
                c.GetValue().source
            );


// (;PW[A. Tari]WR[12k]KM[-59.5]RO[4]RE[B+R]
// PB[B. Lack]BR[5d]PC[London]EV[Go Congress]W[cd]
// C[Game-info:
// Black: B. Lack, 5d
// White: A. Tari, 12k
// Place: London
// Event: Go Congress
// Round: 4
// Komi: -59.5 points
// Result: Black wins by resignation])

            cc = child.children[1];
            Assert.Empty(cc.children);
            Assert.Single(cc.nodes);

            node = cc.nodes[0];

            var km = (Komi) node.props["KM"];
            Assert.Equal(-59.5m, km.GetValue());

            pw = (WhitePlayerName) node.props["PW"];
            Assert.Equal("A. Tari", pw.GetValue().source);

            wr = (WhitePlayerRank) node.props["WR"];
            Assert.Equal("12k", wr.GetValue().source);

            ro = (RoundNumber) node.props["RO"];
            Assert.Equal("4", ro.GetValue().source);

            re = (GameResult) node.props["RE"];
            Assert.Equal(GameResult.WinType.Resign, re.how);
            Assert.Equal(Colour.Black, re.who);
            Assert.Equal(0m, re.score);

            pb = (BlackPlayerName) node.props["PB"];
            Assert.Equal("B. Lack", pb.GetValue().source);

            br = (BlackPlayerRank) node.props["BR"];
            Assert.Equal("5d", br.GetValue().source);

            pc = (Venue) node.props["PC"];
            Assert.Equal("London", pc.GetValue().source);

            ev = (EventName) node.props["EV"];
            Assert.Equal("Go Congress", ev.GetValue().source);

            w = (WhiteMove) node.props["W"];
            Assert.Equal("cd", w.GetValue());

            c = (Comment) node.props["C"];
            Assert.Equal(
@"Game-info:
Black: B. Lack, 5d
White: A. Tari, 12k
Place: London
Event: Go Congress
Round: 4
Komi: -59.5 points
Result: Black wins by resignation",
                c.GetValue().source
            );
        }
    }
}
