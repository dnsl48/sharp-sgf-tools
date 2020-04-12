using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;
using System.Collections.Generic;

namespace dnsl48.SGF.Lexer
{
    /// <summary>
    /// The most basic SGF lexer implementation.
    /// Splits the string data input into a list of lexemes defined by the
    /// structs implemented within the lexer itself.
    /// To be used internally by parsers e.g. <see cref="dnsl48.SGF.NaiveParser" />
    /// </summary>
    public static class NaiveLexer
    {
        /// <summary>
        /// Lexeme of a node property
        /// </summary>
        public readonly struct Property
        {
            /// <summary>
            /// The label of a property.
            /// Usually defined by the SGF standard for all the properties existing.
            /// </summary>
            public readonly string label;

            /// <summary>
            /// The property values.
            /// Different properties may carry different data sets.
            /// Usually defined by the SGF standard for all the properties existing.
            /// </summary>
            public readonly List<string> values;

            /// <summary>
            /// Initialize the lexeme with data
            /// </summary>
            /// <param name="label">The property label</param>
            /// <param name="values">The list of values</param>
            public Property(string label, IEnumerable<string> values) =>
                (this.label, this.values) = (label, new List<string>(values));
        }

        /// <summary>
        /// Lexeme of a node
        /// </summary>
        public readonly struct Node
        {
            /// <summary>
            /// Node property lexemes
            /// </summary>
            public readonly List<Property> properties;

            /// <summary>
            /// Initialize the node with property lexemes
            /// </summary>
            /// <param name="values">Property lexemes</param>
            public Node(IEnumerable<Property> values) =>
                properties = new List<Property>(values);
        }

        /// <summary>
        /// Lexeme of a sequence of nodes
        /// </summary>
        public readonly struct Sequence
        {
            /// <summary>
            /// List of nodes lexemes contained by the sequence
            /// </summary>
            public readonly List<Node> nodes;

            /// <summary>
            /// Initialize the sequence with node lexemes
            /// </summary>
            /// <param name="values">Node lexemes</param>
            public Sequence(IEnumerable<Node> values) =>
                nodes = new List<Node>(values);
        }

        /// <summary>
        /// Lexeme of a tree of node sequences
        /// </summary>
        public readonly struct Tree
        {
            /// <summary>
            /// The parent sequence
            /// </summary>
            public readonly Sequence sequence;

            /// <summary>
            /// The children trees
            /// </summary>
            public readonly List<Tree> children;

            /// <summary>
            /// Initialize the lexeme with the parent sequence and children trees
            /// </summary>
            /// <param name="sequence">The parent sequence</param>
            /// <param name="children">The children trees</param>
            public Tree(Sequence sequence, IEnumerable<Tree> children) =>
                (this.sequence, this.children) = (sequence, new List<Tree>(children));
        }

        private static readonly Parser<char, char> LParen = Char('(');

        private static readonly Parser<char, char> RParen = Char(')');

        private static readonly Parser<char, char> Semicolon = Char(';');

        private static readonly Parser<char, char> LBracket = Char('[');

        private static readonly Parser<char, char> RBracket = Char(']');

        private static readonly Parser<char, char> Backslash = Char('\\');

        /// <value>
        /// Parser for SimpleText defined by the SGF standard.
        /// See: https://www.red-bean.com/sgf/sgf4.html#simpletext
        /// </value>
        public static readonly Parser<char, IEnumerable<string>> ComposedSimpleText = OneOf(
            Try(String("\\\\")).ThenReturn('\\'),
            Try(String("\\]")).ThenReturn(']'),
            Try(String("\\:")).ThenReturn(':'),
            Token(c => c != ':')
        ).ManyString().Separated(Char(':'));

        /// <value>
        /// Parser for property values
        /// </value>
        public static readonly Parser<char, string> PropValue = OneOf(
            Try(String("\\\\")).ThenReturn('\\'),
            Try(String("\\]")).ThenReturn(']'),
            Token(c => c != ']')
        )
        .ManyString()
        .Between(LBracket, RBracket).Between(SkipWhitespaces);

        /// <value>
        /// Parser for property identifier (label)
        /// </value>
        public static readonly Parser<char, string> PropIdent =
            LetterOrDigit
            .ManyString().Between(SkipWhitespaces);

        /// <value>
        /// Parser for the property lexeme (includes identifier and the values)
        /// </value>
        public static readonly Parser<char, Property> PropertyParser =
            from ident in PropIdent
            from values in PropValue.AtLeastOnce()
            select new Property(ident, values);

        /// <value>
        /// Parser for the node lexeme (includes a list of property lexemes)
        /// </value>
        public static readonly Parser<char, Node> NodeParser =
            PropertyParser
                .Many()
                .Select<Node>(props => new Node(props));

        /// <value>
        /// Parser for the sequence lexeme (includes a list of node lexemes)
        /// </value>
        public static readonly Parser<char, Sequence> SequenceParser =
            NodeParser.Between(SkipWhitespaces)
                .Separated(Semicolon)
                .Select<Sequence>(nodes => new Sequence(nodes));

        /// <value>
        /// Parser for the tree lexeme (includes the sequence and children trees recursively)
        /// </value>
        public static readonly Parser<char, Tree> TreeParser =
            from _open in LParen.Between(SkipWhitespaces)
            from seq in SequenceParser
            from children in Rec(() => TreeParser).Many()
            from _close in RParen.Between(SkipWhitespaces)
            select new Tree(seq, children)
        ;
    }
}
