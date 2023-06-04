namespace WebAppWithCRUD.dto
{
    /// <summary>
    /// this class holds information regarding validation errors.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// gets or sets the field that failed validation.
        /// </summary>
        /// <value>string.</value>
        public string FieldName { get; set; }

        /// <summary>
        /// gets or sets the detail of the validation failure.
        /// </summary>
        /// <value>string.</value>
        public string ErrorDetail { get; set; }
    }
}