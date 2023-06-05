using WebAppWithCRUD.dto;
using WebAppWithCRUD.dto.Request;
using WebAppWithCRUD.dto.Response;
using WebAppWithCRUD.Models;

namespace WebAppWithCRUD.Services.Interfaces
{
    /// <summary>
    /// Interface for the <see cref="ClientService" /> class
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Gets all clients from the repository.
        /// </summary>
        /// <returns>
        /// The ServiceResponse with the list of ClientItemResponse if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        Task<ServiceResponse<IReadOnlyList<ClientItemResponse>>> GetAllAsync();

        /// <summary>
        /// Gets the client by id.
        /// </summary>
        /// <param name="id">client id.</param>
        /// <returns>
        /// The ServiceResponse with the ClientItemResponse if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        Task<ServiceResponse<ClientItemResponse>> GetByIdAsync(int id);

        /// <summary>
        /// Insert the new client.
        /// </summary>
        /// <param name="request">InsertClientRequest.</param>
        /// <returns>
        /// The ServiceResponse with the client id if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        Task<ServiceResponse<int>> InsertBySPAsync(InsertClientRequest request);

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="request">UpdateClientRequest.</param>
        /// <returns>
        /// The ServiceResponse with the bool if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        Task<ServiceResponse<bool>> UpdateBySPAsync(int id, UpdateClientRequest request);

        /// <summary>
        /// Delete the client by id.
        /// </summary>
        /// <param name="id">client id.</param>
        /// <returns>
        /// The ServiceResponse with the bool if the request
        /// was succesfull or with the corresponding error otherwise.
        /// </returns>
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
