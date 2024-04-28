using MySql.Data.MySqlClient;
using System.Data;

namespace HCM.Services.Interfaces
{
    /// <summary>
    /// Database connectivity Service contract from the REST API calls.
    /// </summary>
    public interface IDBServiceBase
    {
        public MySqlConnection GetDatabaseConnection();
        public MySqlCommand GetDatabaseCommand(CommandType commandType, string commandText, Dictionary<string, object>? commandParameters = null);
    }
}
