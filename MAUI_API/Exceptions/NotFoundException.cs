namespace MAUI_API.Exceptions
{
    /// <summary>
    /// Fires when the element is not found in the database.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Fires when the element is not found in the database.
        /// </summary>
        public NotFoundException()
        {

        }
        /// <summary>
        /// Fires when the element is not found in the database.
        /// </summary>
        /// <param name="message">Specification for the item you are looking for.</param>
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
