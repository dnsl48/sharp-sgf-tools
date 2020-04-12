namespace dnsl48.SGF.Model.Property
{
    /// <summary>
    /// Interface for properties containing a position
    /// </summary>
    public interface IPosition
    {
        /// <summary>Returns X axis of the position</summary>
        byte GetX();

        /// <summary>Returns Y axis of the position</summary>
        byte GetY();
    }
}
