using VRCore.Entities;

namespace VRDatabase.Interfaces
{
    public interface IDbContext : IDisposable
    {
        string ConnectionString { get; }
    }
}