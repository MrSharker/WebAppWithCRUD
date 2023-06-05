using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
        /// gets the database facade of the context.
        /// </summary>
        /// <value>DatabaseFacade.</value>
        DatabaseFacade Database { get; }

        /// <summary>
        /// this method executes the save changes method of the context.
        /// </summary>
        /// <param name="cancellationToken">teh cancellation token for cancelling async operations.</param>
        /// <returns>int.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
