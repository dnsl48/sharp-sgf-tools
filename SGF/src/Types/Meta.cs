using System;
using System.Collections.Generic;
using System.Linq;

namespace dnsl48.SGF.Types
{
    /// <summary>
    /// The library types metadata container.
    /// Provides a simple API to access the implemented types at runtime.
    /// Implements dynamic autodiscovery of all the available type implementations
    /// that gets run once at runtime when initializing the static state of this class.
    /// </summary>
    public static class Meta
    {
        /// <value>
        /// List with all the type implementations available to the library
        /// </value>
        public static readonly List<Type> PropValueTypes = _GetPropValueTypes();

        private static List<Type> _GetPropValueTypes()
        {
            return new List<Type>(typeof(Meta).Assembly.DefinedTypes.Where(t => t.GetInterface("IPropertyValue") != null));
        }
    }
}
