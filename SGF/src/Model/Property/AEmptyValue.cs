using System.Collections.Generic;

namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// A property with no explicit values (it only has its label and may have some assigned attributes)
    /// </summary>
    public abstract class AEmptyValue<T>: AProperty, IEmptyValue
    where T: AEmptyValue<T>
    {
        /// <summary>
        /// Construct the empty property
        /// </summary>
        public AEmptyValue() {}

        /// <inheritdoc />
        public override string StringValue()
        {
            return $"{GetLabel()}[]";
        }

        /// <summary>
        /// Parse the list of property values and build the property instance.
        /// The current implementation checks there are no values in the list.
        /// </summary>
        /// <param name="values">List of the lexemes of the values to init the property instance with</param>
        /// <returns>An instance of the property constructed with the values given</returns>
        public static IProperty Parse(List<string> values)
        {
            if (values.Count > 1)
                return null;

            if (values.Count == 1 && values[0].Trim() != "")
                return null;

            var ctor = typeof(T).GetConstructor(new System.Type[] {});
            if (ctor == null)
                return null;

            try {
                return (IProperty) ctor.Invoke(new object[] { });
            } catch (System.Exception) {
                return null;
            }
        }
    }
}
