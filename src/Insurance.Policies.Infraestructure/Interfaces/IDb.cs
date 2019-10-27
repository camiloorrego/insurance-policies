using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Insurance.Policies.Infraestructure.Interfaces
{
    public interface IDb
    {
        SqlConnection Connection { get; set; }
        Task<IList<T>> SelectAsync<T>(string sql, object parameters);
        Task ExecuteAsync(string sql, object parameters);
    }
}
