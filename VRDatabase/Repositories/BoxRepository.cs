using System.Data.SqlClient;
using VRCore.Entities;
using VRDatabase.Interfaces;
using VRDatabase.Repositories.Interfaces;

namespace VRDatabase.Repositories
{
    public class BoxRepository : IBoxRepository
    {
        private readonly IDbContext Context;

        public BoxRepository(IDbContext context)
        {
            Context = context;
        }

        public Task BulkInsertAsync(IList<Box> boxes)
        {
            using (var connection = new SqlConnection(Context.ConnectionString))
            {
                // Create sql, map the objects into it
                return Task.FromResult(0);
            }
        }
    }
}
