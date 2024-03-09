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
            Context.Boxes.AddRange(boxes);
            
            return Task.FromResult(0);
        }

        public Task<bool> Exists(Func<Box, bool> predicate)
        {
            var count = Context.Boxes.Count(predicate);

            return Task.FromResult(count > 0);
        }        
    }
}
