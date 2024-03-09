using VRCore.Entities;

namespace VRDatabase.Repositories.Interfaces
{
    public interface IBoxRepository
    {
        Task BulkInsertAsync(IList<Box> boxes);

        Task<bool> Exists(Func<Box, bool> predicate);
    }
}