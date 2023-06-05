using System.ComponentModel.DataAnnotations;
using WebAppWithCRUD.Enums;

namespace WebAppWithCRUD.Models
{
    public class Client
    {
        /// <summary>
        /// gets or sets the Id of the client.
        /// </summary>
        /// <value>int.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// gets or sets the Name of the client.
        /// </summary>
        /// <value>string.</value>
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// gets or sets the Email of the client.
        /// </summary>
        /// <value>string.</value>
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        /// <summary>
        /// gets or sets the PhoneExtension of the client.
        /// </summary>
        /// <value>string.</value>
        [Required]
        [MinLength(1),MaxLength(3)]
        public string PhoneExtension { get; set; }

        /// <summary>
        /// gets or sets the PhoneNumber of the client.
        /// </summary>
        /// <value>string.</value>
        [Required]
        [StringLength(9)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// gets or sets the EmailStatus of the client.
        /// </summary>
        /// <value>int.</value>
        public int EmailStatus { get; set; } = (int)StatusEnum.Active;

        /// <summary>
        /// gets or sets the SmsStatus of the client.
        /// </summary>
        /// <value>int.</value>
        public int SmsStatus { get; set; } = (int)StatusEnum.Active;

        /// <summary>
        /// gets or sets the CreateDate of the client.
        /// </summary>
        /// <value>DateTime.</value>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// gets or sets the UpdateDate of the client.
        /// </summary>
        /// <value>DateTime.</value>
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
