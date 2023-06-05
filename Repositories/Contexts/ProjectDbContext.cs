using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WebAppWithCRUD.Models;
using WebAppWithCRUD.Repositories.Interfaces;

namespace WebAppWithCRUD.Repositories.Contexts
{
    /// <summary>
    /// this class defined the Project Db Context.
    /// </summary>
    public class ProjectDbContext : DbContext, IProjectDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDbContext" /> class.
        /// </summary>
        /// <param name="options">the options for the dbContext.</param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDbContext" /> class.
        /// </summary>
        public ProjectDbContext() { }

        /// <summary>
        /// gets or sets the Client dbset.
        /// </summary>
        /// <value>DbSet.</value>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// this method overrides the OnModelCreating of the base class (DbContext).
        /// </summary>
        /// <param name="modelBuilder">the model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasIndex(c => new { c.Email, c.PhoneNumber }).IsUnique();
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
