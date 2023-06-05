using WebAppWithCRUD.Models;

namespace WebAppWithCRUD.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the <see cref="ClientRepository" /> class
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Gets all Clients from the database.
        /// </summary>
        /// <returns>The list of clients.</returns>
        Task<IReadOnlyList<Client>> GetAllAsync();

        /// <summary>
        /// Gets client by the id.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>Client.</returns>
        Task<Client> GetByIdAsync(int id);

        /// <summary>
        /// Check if client exists by id.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>bool.</returns>
        Task<bool> IsClientExists(int id);

        /// <summary>
        /// Delete the client.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>Void.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Insert new client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>(int id,string error).</returns>
        Task<(int id,string error)> InsertBySPAsync(Client client);

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>string.</returns>
        Task<string> UpdateBySPAsync(Client client);
    }
}
