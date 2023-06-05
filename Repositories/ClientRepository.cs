using Microsoft.Data.SqlClient;
using System.Data;
using WebAppWithCRUD.Models;
using WebAppWithCRUD.Repositories.Interfaces;

namespace WebAppWithCRUD.Repositories
{
    /// <summary>
    /// Repository that handles database access to the <see cref="Client" /> model.
    /// </summary>
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;

        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The repository logger.</param>
        public ClientRepository( IConfiguration configuration,
            ILogger<ClientRepository> logger)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("SqlConnection");
        }

        /// <summary>
        /// Gets all Clients from the database.
        /// </summary>
        /// <returns>The list of clients.</returns>
        public async Task<IReadOnlyList<Client>> GetAllAsync()
        {
            List<Client> clients = new List<Client>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Clients", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clients.Add(MapToClient(reader));
                        }
                    }
                }
            }

            return clients;
        }

        /// <summary>
        /// Gets client by the id.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>Client.</returns>
        public async Task<Client> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Clients WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapToClient(reader);
                        }

                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Delete the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>Void.</returns>
        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("DELETE FROM Clients WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    // Execute the DELETE query asynchronously
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Insert new client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>(int id,string error).</returns>
        public async Task<(int id, string error)> InsertBySPAsync(Client client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("InsertClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", client.Name);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@PhoneExtension", client.PhoneExtension);
                    command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    SqlParameter clientIdParam = new SqlParameter("@ClientId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(clientIdParam);
                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(errorMessageParam);
                    await command.ExecuteNonQueryAsync();

                    return ((int)clientIdParam.Value, (string)errorMessageParam.Value);
                }
            }

        }

        /// <summary>
        /// Check if client exists by id.
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>bool.</returns>
        public async Task<bool> IsClientExists(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Clients WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Update the client.
        /// </summary>
        /// <param name="client">client model</param>
        /// <returns>string.</returns>
        public async Task<string> UpdateBySPAsync(Client client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", client.Id);
                    command.Parameters.AddWithValue("@Name", client.Name);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@PhoneExtension", client.PhoneExtension);
                    command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    command.Parameters.AddWithValue("@EmailStatus", client.EmailStatus);
                    command.Parameters.AddWithValue("@SmsStatus", client.SmsStatus);
                    SqlParameter errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(errorMessageParam);
                    await command.ExecuteNonQueryAsync();

                    return (string)errorMessageParam.Value;
                }
            }
        }

        /// <summary>
        /// Map data to client.
        /// </summary>
        /// <param name="reader">Sql data reader</param>
        /// <returns>Client.</returns>
        private Client MapToClient(SqlDataReader reader)
        {
            return new Client
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                PhoneExtension = (string)reader["PhoneExtension"],
                PhoneNumber = (string)reader["PhoneNumber"],
                EmailStatus = (int)reader["EmailStatus"],
                SmsStatus = (int)reader["SmsStatus"],
                CreateDate = (DateTime)reader["CreateDate"],
                UpdateDate = (DateTime)reader["UpdateDate"],
            };
        }
    }
}
