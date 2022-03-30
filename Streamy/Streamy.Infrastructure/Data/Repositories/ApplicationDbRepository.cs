using Streamy.Infrastructure.Data.Common;

namespace Streamy.Infrastructure.Data.Repositories
{
    public class ApplicationDbRepository : Repository, IApplicationDbRepository
    {
        public ApplicationDbRepository(ApplicationDbContext context)
        {
            Context = context;
        }
    }
}
