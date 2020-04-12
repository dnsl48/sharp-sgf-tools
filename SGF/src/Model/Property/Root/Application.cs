using Pidgin;

using System.Collections.Generic;

using dnsl48.SGF.Attributes;
using dnsl48.SGF.Lexer;
using dnsl48.SGF.Types;

namespace dnsl48.SGF.Model.Property.Root
{
    /// <summary>
    /// A node property implementation.
    /// Follows the SGF specification: https://www.red-bean.com/sgf/properties.html#AP
    /// </summary>
    [Label("AP")]
    [Description("Name and version number of the application used to create this")]
    public class Application: AProperty
    {
        /// <value>Application name</value>
        public SimpleComposedText app { get; }

        /// <value>Application version</value>
        public SimpleComposedText version { get; }

        /// <summary>Initialize the property</summary>
        public Application(SimpleComposedText app, SimpleComposedText version) {
            this.app = app;
            this.version = version;
        }

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"AP[{app}:{version}]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count != 1)
                return null;

            try {
                var parser = NaiveLexer.ComposedSimpleText.Select<List<string>>(ss => new List<string>(ss));
                var parts = parser.Parse(values[0]);

                if (!parts.Success)
                    return null;

                if (parts.Value.Count != 2)
                    return null;

                return new Application(
                    SimpleComposedText.parse(parts.Value[0]),
                    SimpleComposedText.parse(parts.Value[1])
                );

            } catch (System.Exception) {
                return null;
            }
        }
    }
}
