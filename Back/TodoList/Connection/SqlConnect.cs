using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace TodoList.Connection
{
    public class SqlConnect: DbContext
    {
        private readonly IConfiguration configuration;

        public SqlConnect(IConfiguration config)
        {
            this.configuration = config;
        }

        public SqlConnection GetSqlConnection()
        {
            string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            return connection;
        }
    }
}
