using System.Data;

namespace Discount.Core.Core
{
    public interface IDapperContext
    {
        IDbConnection CreateNpgsqlConnection();
    }
}
