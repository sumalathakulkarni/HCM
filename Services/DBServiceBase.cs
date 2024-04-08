using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace HCM.Services
{
    public class DBServiceBase : IDBServiceBase
    {

        private IConfiguration _configuration;

        public DBServiceBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetDatabaseConnection()
        {
            string connectionString = _configuration.GetConnectionString(HCMConstants.DBConnectionStringSetting);
            return new MySqlConnection(connectionString);
        }

        public MySqlCommand GetDatabaseCommand(CommandType commandType, string commandText, Dictionary<string, object>? commandParameters = null)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.CommandType = commandType;

            if (commandParameters != null)
            {
                foreach (var value in commandParameters)
                {
                    sqlCommand.Parameters.Add(new MySqlParameter(value.Key, value.Value));
                }
            }
            return sqlCommand;
        }
    }
}
