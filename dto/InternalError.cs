namespace WebAppWithCRUD.dto
{
    /// <summary>
    /// this class describes an error inside the application for transfer over the network.
    /// </summary>
    public class InternalError
    {
        /// <summary>
        /// gets or sets the location (method name) the error occurred.
        /// </summary>
        /// <value>string.</value>
        public string Location { get; set; }

        /// <summary>
        /// gets or sets the message of the error.
        /// </summary>
        /// <value>string.</value>
        public string Message { get; set; }
    }
}