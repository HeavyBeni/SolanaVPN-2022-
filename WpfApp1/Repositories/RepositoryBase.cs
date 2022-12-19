using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp1.Repositories
{
    // Connection to the Database that is used by UserRepository...
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        
        // Connectionstring used in UserRepository for acessing database...
        public RepositoryBase()
        {
            _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\benjamin\\Source\\Repos\\SolanaVPN-2022-\\WpfApp1\\Database1.mdf;Integrated Security=True";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
