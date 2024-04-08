using MySql.Data.MySqlClient;
using System.Data;

namespace HCM.Services.Interfaces
{
    public interface IDBServiceBase
    {
        public MySqlConnection GetDatabaseConnection();
        public MySqlCommand GetDatabaseCommand(CommandType commandType, string commandText, Dictionary<string, object>? commandParameters = null);
    }
}
