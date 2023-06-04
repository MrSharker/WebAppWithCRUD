namespace WebAppWithCRUD.dto
{
    /// <summary>
    /// this class defines a generic response from a service layer class.
    /// </summary>
    /// <typeparam name="T">the type of the response payload.</typeparam>
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse{T}" /> class.
        /// </summary>
        /// <param name="value">the payload.</param>
        public ServiceResponse(T value)
        {
            this.Payload = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse{T}" /> class.
        /// </summary>
        /// <param name="internalError">the exception that occured when processing the request.</param>
        public ServiceResponse(InternalError internalError)
        {
            this.Exception = internalError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceResponse{T}" /> class.
        /// </summary>
        /// <param name="validationErrors">the validation errors of the passed model.</param>
        public ServiceResponse(IReadOnlyList<ValidationError> validationErrors)
        {
            this.Errors = validationErrors;
        }

        /// <summary>
        /// gets or sets the list of validation errors for the passed model.
        /// </summary>
        /// <returns>readonly list of validation errors.</returns>
        public IReadOnlyList<ValidationError> Errors { get; set; } = new List<ValidationError>();

        /// <summary>
        /// gets or sets the payload of the response.
        /// </summary>
        /// <value>generic.</value>
        public T Payload { get; set; }

        /// <summary>
        /// gets or sets a class containing information on the exception that occured while proccessing the request.
        /// </summary>
        /// <value>internal error.</value>
        public InternalError Exception { get; set; }

        /// <summary>
        /// gets a value indicating whether the request ended with an exception.
        /// </summary>
        public bool IsException => this.Exception != null;

        /// <summary>
        /// gets a value indicating whether the request was successfully processed.
        /// </summary>
        /// <returns>boolean.</returns>
        public bool IsSuccessful => !this.Errors.Any() && this.Exception == null;
    }
}
