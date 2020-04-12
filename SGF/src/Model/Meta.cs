using dnsl48.SGF.Attributes;
using System;
using System.Collections.Generic;

namespace dnsl48.SGF.Model
{
    /// <summary>
    /// The library model metadata container.
    /// Provides a simple API to access the implemented property library at runtime.
    /// Implements dynamic autodiscovery of all the available property implementations
    /// that gets run once at runtime when initializing the static state of the class.
    /// </summary>
    public static class Meta
    {
        /// <value>
        /// Dictionary with all the property implementations available to the library.
        /// Keys are the property labels, values are the property types.
        /// </value>
        public static readonly Dictionary<string, Type> PropertyTypes = _GetPropertyTypes();

        private static Dictionary<string, Type> _GetPropertyTypes()
        {
            var dict = new Dictionary<string, Type>();

            foreach (var item in typeof(AProperty).Assembly.DefinedTypes)
            {
                var label = Attribute.GetCustomAttribute(item, typeof(LabelAttribute));

                if (label != null)
                {
                    dict.Add(label.ToString(), item);
                }
            }

            return dict;
        }
    }
}
