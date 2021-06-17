using Microsoft.EntityFrameworkCore;

namespace Jammehcow.YosherBot.EfCore.Repositories
{
    public class Repository<TContext> where TContext : DbContext
    {
        /// <summary>
        /// The underlying DbContext used by the repo
        /// </summary>
        public TContext Context;

        public Repository(TContext dbContext)
        {
            Context = dbContext;
        }
    }
}
