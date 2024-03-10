using System.Configuration;
using VRDatabase.Interfaces;

namespace VRDatabase
{
    public sealed class DbContext : IDbContext
    {
        private string? _connectionString;

        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["VR"]?.ToString();

                    if (_connectionString == null)
                    {
                        throw new Exception("ConnectionString VR not found!");
                    }
                }

                return _connectionString;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
