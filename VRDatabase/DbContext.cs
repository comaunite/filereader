using VRCore.Entities;
using VRDatabase.Interfaces;

namespace VRDatabase
{
    public sealed class DbContext : IDbContext
    {
        public List<Box> Boxes { get; private set; }

        public DbContext()
        {
            Boxes = [];
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
