namespace dnsl48.SGF.Model
{
    /// <summary>
    /// The colour primitive containing black/white identity
    /// </summary>
    public enum Colour : byte
    {
        /// <value>Black colour</value>
        Black,

        /// <value>White colour</value>
        White
    }

    /// <summary>
    /// Static methods for the <see cref="Colour"/> primitive
    /// </summary>
    public static class _ColourImpl
    {
        /// <summary>
        /// Encode the colour primitive into its SGF encoded text value
        /// See: https://www.red-bean.com/sgf/sgf4.html#types
        /// </summary>
        /// <param name="colour">The colour to encode</param>
        /// <exception cref="System.Exception">If undefined byte has been used for the colour</exception>
        public static string GetLabel(this Colour colour)
        {
            switch (colour)
            {
                case Colour.Black:
                    return "B";

                case Colour.White:
                    return "W";

                default:
                    throw new System.Exception("Unknown colour");
            }
        }
    }
}
