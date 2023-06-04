using Microsoft.EntityFrameworkCore;
using WebAppWithCRUD.Models;

namespace WebAppWithCRUD.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the <see cref="ProjectDbContext" /> class.
    /// </summary>
    public interface IProjectDbContext
    {
        /// <summary>
        /// gets or sets the Client dbset.
        /// </summary>
        /// <value>DbSet.</value>
        DbSet<Client> Clients { get; set; }

        /// <summary>
        /// this method executes the save changes method of the context.
        /// </summary>
        /// <param name="cancellationToken">teh cancellation token for cancelling async operations.</param>
        /// <returns>int.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
