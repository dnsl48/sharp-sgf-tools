using dnsl48.SGF.Attributes;
using System.Collections.Generic;

namespace dnsl48.SGF.Model
{
    /// <summary>
    /// Abstract IProperty implementation.
    /// Implements <see cref="AProperty.GetLabel()"/> and <see cref="AProperty.GetDescription()"/>
    /// methods by looking up the statically assigned <see cref="LabelAttribute"/>
    /// and <see cref="DescriptionAttribute"/> accordingly.
    /// Leaves <see cref="AProperty.StringValue()"/> unimplemented so that every property
    /// implements its own syntax.
    /// </summary>
    public abstract class AProperty : IProperty
    {
        /// <inheritdoc />
        public abstract string StringValue();

        /// <inheritdoc />
        public string GetLabel()
        {
            var attr = System.Attribute.GetCustomAttribute(this.GetType(), typeof(LabelAttribute));

            if (attr != null) {
                return attr.ToString();
            }

            return " ";
        }

        /// <inheritdoc />
        public string GetDescription()
        {
            var attr = System.Attribute.GetCustomAttribute(this.GetType(), typeof(DescriptionAttribute));

            if (attr != null) {
                return attr.ToString();
            }

            return " ";
        }
    }
}
