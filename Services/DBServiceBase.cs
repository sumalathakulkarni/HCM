using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;

namespace HCM.Services
{
    /// <summary>
    /// MySQL Database Connectivity Service (Helper Class) for all the REST API calls.
    /// </summary>
    public class DBServiceBase : IDBServiceBase
    {

        private IConfiguration _configuration; //Configuration instance declaration. 

        public DBServiceBase(IConfiguration configuration)
        {
            //Configuration instance initialization.
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the Database connectionstring from configuration settings.
        /// </summary>
        /// <returns>string: MySqlConnection object with the connectionstring => MySQL database connectionstring from the appsettings.json file.</returns>
        public MySqlConnection GetDatabaseConnection()
        {
            
            string connectionString = _configuration.GetConnectionString(HCMConstants.DBConnectionStringSetting);
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Retrieves the database command for passing to the Database.
        /// </summary>
        /// <param name="commandType">SQL Query/ Stored Procedure</param>
        /// <param name="commandText">SQL Query/ Procedure Name</param>
        /// <param name="commandParameters">Parameters for the query/ stored procedure</param>
        /// <returns>MysqlCommand</returns>
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
