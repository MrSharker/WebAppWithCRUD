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
        /// Gets client by the email.
        /// </summary>
        /// <param name="email">client email</param>
        /// <returns>Client.</returns>
        Task<Client> GetByEmailAsync(string email);

        /// <summary>
        /// Check if email already exists in db.
        /// </summary>
        /// <param name="email">client email</param>
        /// <returns>bool.</returns>
        Task<bool> IsEmailExistAsync(string email);

        /// <summary>
        /// Check if record with pair of email and phone number already exists in db.
        /// </summary>
        /// <param name="email">client email</param>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>bool.</returns>
        Task<bool> IsEmailAndPhoneExistAsync(string email, string phoneNumber);

        /// <summary>
        /// Check if record with phone number already exists in db.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>bool.</returns>
        Task<bool> IsPhoneExistAsync(string phoneNumber);

        /// <summary>
        /// Insert new client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>int.</returns>
        Task<int> InsertAsync(Client client);

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>Void.</returns>
        Task UpdateAsync(Client client);

        /// <summary>
        /// Delete the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>Void.</returns>
        Task DeleteAsync(Client client);

        /// <summary>
        /// Get sms status by the phone number.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>int.</returns>
        Task<int> CheckSmsStatus(string phoneNumber);

        /// <summary>
        /// Update the sms status for the all records with phone number.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <param name="status">client sms status</param>
        /// <returns>Void.</returns>
        Task UpdateSmsStatusAsync(string phoneNumber, int status);
    }
}
