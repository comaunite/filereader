using VRCore.Entities;

namespace VRDatabase.Repositories.Interfaces
{
    public interface IBoxRepository
    {
        Task BulkInsertAsync(IList<Box> boxes);
    }
}