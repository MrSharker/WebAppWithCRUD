namespace WebAppWithCRUD.dto.Request
{
    /// <summary>
    /// this model is used when adding an client.
    /// </summary>
    public class InsertClientRequest
    {
        /// <summary>
        /// gets or sets the name of the client.
        /// </summary>
        /// <value>string.</value>
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the Email of the client.
        /// </summary>
        /// <value>string.</value>
        public string Email { get; set; }

        /// <summary>
        /// gets or sets the Cellphone of the client.
        /// </summary>
        /// <value>string.</value>
        public string Cellphone { get; set; }
    }
}
