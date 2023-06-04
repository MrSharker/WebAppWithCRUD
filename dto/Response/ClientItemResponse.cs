using System.ComponentModel.DataAnnotations;
using WebAppWithCRUD.Enums;

namespace WebAppWithCRUD.dto.Response
{
    /// <summary>
    /// this model is used when returning a list of clients.
    /// </summary>
    public class ClientItemResponse
    {
        /// <summary>
        /// gets or sets the Id of the client.
        /// </summary>
        /// <value>int.</value>
        public int Id { get; set; }
        /// <summary>
        /// gets or sets the Name of the client.
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

        /// <summary>
        /// gets or sets the EmailStatus of the client.
        /// </summary>
        /// <value>int.</value>
        public int EmailStatus { get; set; }

        /// <summary>
        /// gets or sets the SmsStatus of the client.
        /// </summary>
        /// <value>int.</value>
        public int SmsStatus { get; set; }
    }
}
