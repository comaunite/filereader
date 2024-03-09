using VRCore.Entities;

namespace VRDatabase.Interfaces
{
    public interface IDbContext : IDisposable
    {
        List<Box> Boxes { get; }
    }
}