using WebAppWithCRUD.Repositories.Interfaces;

namespace WebAppWithCRUD.Repositories
{
    public class BaseRepository
    {
        /// <summary>
        /// gets the database context.
        /// </summary>
        /// <value>IProjectDbContext.</value>
        protected IProjectDbContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        /// <param name="context">the database context.</param>
        public BaseRepository(IProjectDbContext context)
        {
            Context = context;
        }
    }
}
