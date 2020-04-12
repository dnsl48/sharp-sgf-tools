# Sharp SGF tools (C#)

C# library for Simple Game Format, including parser and generator
------

[![CI](https://github.com/dnsl48/sharp-sgf-tools/workflows/CI/badge.svg)](https://github.com/dnsl48/sharp-sgf-tools/actions?query=workflow%3ACI) ![MIT / Apache2 License](https://img.shields.io/badge/license-MIT%20/%20Apache2-blue.svg)
------

## Features

 - [SGF v4](https://www.red-bean.com/sgf/) parsing and generation
 - Strictly typed data model
 - Supports all [data types](https://www.red-bean.com/sgf/sgf4.html) (incl compressed point lists)
 - Out of the box implementations for all properties needed for Go ([GM[1]](https://www.red-bean.com/sgf/go.html))


### SGF v4 parsing and generation

#### Parsing example

```csharp
using dnsl48.SGF.Model;
using dnsl48.SGF.Model.Property;

// Parser state keeps all errors
var parser = new dnsl48.SGF.NaiveParser();

try {
    // The SGF tree collection
    dnsl48.SGF.Model.Collection collection = parser.Parse(sgfBlob); 
catch (dnsl48.SGF.NaiveParser.IParseError e) {
    // incorrect SGF document
} catch (Exception e) {
    // unexpected runtime exception
}


// Strictly typed model for working with the tree
// It's all have already been validated when parsed
try {
    var node = collection.trees[0].nodes[0];

    // SZ[19] becomes Model.Property.Root.BoardSize
    if (node.props.has("SZ")) {
        Root.BoardSize size = (Root.BoardSize) node.props["SZ"];
        size.x == 19;
        size.y == 19;
    }

    // RE[W+3.5] becomes Model.Property.Info.GameResult
    if (node.props.ContainsKey("RE")) {
        Info.GameResult result = (Info.GameResult) node.props["RE"];
        if (result.how == GameResult.WinType.Score) {
            result.who == Colour.White;  // White won
            result.score == 3.5m;  // how much
        }
    }
}
```


#### Generation example

```csharp
using dnsl48.SGF.Model;
using dnsl48.SGF.Types;

// Use Model to generate SGF

var collection = new Collection(
    new Tree[] { // the sequence of collection trees
        new Tree(
            new Node[] { // the sequence of tree nodes
                new Node(
                    new IProperty[] { // the sequence of node properties

                        // Application becomes "AP"
                        (IProperty) new Application(
                            new SimpleComposedText("Sharp SGF tools"),
                            new SimpleComposedText("0.0.1")
                        ),

                        // GameResult becomes "RE"
                        (IProperty) new GameResult(Colour.Black, 8.5m),

                        // GameType becomes "GM"
                        (IProperty) new GameType(1)
                    })
            },
            new Tree[] {} // the child trees go here
        )
    }
);

string sgfBlob = collection.ToString();

// sgfBlob contains the following: "(;AP[Sharp SGF tools:0.0.1] RE[B+8.5] GM[1])"

```
