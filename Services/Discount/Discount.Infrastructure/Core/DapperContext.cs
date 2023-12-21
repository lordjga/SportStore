using Discount.Core.Core;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Discount.Infrastructure.Core
{
    public class DapperContext: IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateNpgsqlConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
