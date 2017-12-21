using System.Data;

namespace ColorsTest.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection Get(string connectionString);
    }
}