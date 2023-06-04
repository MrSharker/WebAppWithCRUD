using Microsoft.EntityFrameworkCore;
using WebAppWithCRUD.Models;
using WebAppWithCRUD.Repositories.Interfaces;

namespace WebAppWithCRUD.Repositories
{
    /// <summary>
    /// Repository that handles database access to the <see cref="Client" /> model.
    /// </summary>
    public class ClientRepository : BaseRepository, IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository" /> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The repository logger.</param>
        public ClientRepository( IProjectDbContext context,
            ILogger<ClientRepository> logger) : base(context)
        {
            _logger = logger;
        }

        /// <summary>
        /// Delete the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>Void.</returns>
        public async Task DeleteAsync(Client client)
        {
            this.Context.Clients.Remove(client);
            await this.Context.SaveChangesAsync(default);
        }

        /// <summary>
        /// Gets all Clients from the database.
        /// </summary>
        /// <returns>The list of clients.</returns>
        public async Task<IReadOnlyList<Client>> GetAllAsync()
        {
            return await this.Context.Clients.ToListAsync();
        }

        /// <summary>
        /// Gets client by the email.
        /// </summary>
        /// <param name="email">client email</param>
        /// <returns>Client.</returns>
        public async Task<Client> GetByEmailAsync(string email)
        {
            return await this.Context.Clients.Where(c => c.Email == email).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets client by the id.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>Client.</returns>
        public async Task<Client> GetByIdAsync(int id)
        {
            return await this.Context.Clients.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Insert new client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>int.</returns>
        public async Task<int> InsertAsync(Client client)
        {
            await this.Context.Clients.AddAsync(client);
            await this.Context.SaveChangesAsync(default);

            return client.Id;
        }
        /// <summary>
        /// Check if email already exists in db.
        /// </summary>
        /// <param name="email">client email</param>
        /// <returns>bool.</returns>
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await this.Context.Clients.AnyAsync(c => c.Email == email);
        }

        /// <summary>
        /// Check if record with pair of email and phone number already exists in db.
        /// </summary>
        /// <param name="email">client email</param>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>bool.</returns>
        public async Task<bool> IsEmailAndPhoneExistAsync(string email, string phoneNumber)
        {
            return await this.Context.Clients.AnyAsync(c => c.Email == email && c.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// Check if record with phone number already exists in db.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>bool.</returns>
        public async Task<bool> IsPhoneExistAsync(string phoneNumber)
        {
            return await this.Context.Clients.AnyAsync(c => c.PhoneNumber == phoneNumber);
        }

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>Void.</returns>
        public async Task UpdateAsync(Client client)
        {
            this.Context.Clients.Update(client);
            await this.Context.SaveChangesAsync(default);
        }

        /// <summary>
        /// Get sms status by the phone number.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <returns>int.</returns>
        public async Task<int> CheckSmsStatus(string phoneNumber)
        {
            return (await this.Context.Clients
                .Where(c => c.PhoneNumber == phoneNumber).FirstAsync()).SmsStatus;
        }

        /// <summary>
        /// Update the sms status for the all records with phone number.
        /// </summary>
        /// <param name="phoneNumber">client phone number</param>
        /// <param name="status">client sms status</param>
        /// <returns>Void.</returns>
        public async Task UpdateSmsStatusAsync(string phoneNumber, int status)
        {
            var clients = await this.Context.Clients.Where(c => c.PhoneNumber == phoneNumber).ToListAsync();

            foreach (var client in clients)
            {
                client.SmsStatus = status;
            }

            this.Context.Clients.UpdateRange(clients);
        }
    }
}
