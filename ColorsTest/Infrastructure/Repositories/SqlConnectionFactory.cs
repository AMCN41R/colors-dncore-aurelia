using System.Data;
using System.Data.SqlClient;
using ColorsTest.Core;

namespace ColorsTest.Repositories
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Get(string connectionString)
        {
            Guard.AgainstNullOrWhitespaceArgument(connectionString, nameof(connectionString));

            return new SqlConnection(connectionString);
        }
    }
}