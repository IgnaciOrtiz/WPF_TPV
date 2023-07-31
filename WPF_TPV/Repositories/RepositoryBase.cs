using Microsoft.Data.SqlClient;

namespace WPF_LoginForm.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=TPV; Integrated Security=true; TrustServerCertificate=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}