
using HCM.Models;
using HCM.Services.Interfaces;
using HCM.Utilities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace HCM.Services
{
    /// <summary>
    /// Service for the Database Service calls for all the REST API calls of the Skill module views and functionalities.
    /// </summary>
    public class SkillService : DBServiceBase, ISkillService
    {
        public SkillService(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Responsible for retrieving the list of all skills from the database.
        /// </summary>
        /// <returns>List of SkillModel objects.</returns>
        public IList<SkillModel> GetAllSkills()
        {
            var Skills = new List<SkillModel>();
            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.GetAllSkills);
            cmd.Connection = con;
            try
            {
                con.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SkillModel skill = new SkillModel()
                    {
                        SkillID = reader.GetInt32("SkillID"),
                        SkillName = reader.GetString("SkillName"),
                    };
                    Skills.Add(skill);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                con.Close();
            }
            return Skills;
        }

        /// <summary>
        /// Responsible for saving the Skill details to the database in the Add New Skill/ Edit Skill scenarios.
        /// </summary>
        /// <param name="skill">SkillModel object with the attribute values set from the input field values from the UI.</param>
        /// <returns>result success/ failure</returns>
        public int SaveSkill(SkillModel skill)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "skillName", skill.SkillName }
            };

            var con = GetDatabaseConnection();

            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.AddSkill, parameters);
            if (skill.SkillID != 0)
            {
                parameters.Add("SID", skill.SkillID);
                cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.UpdateSkillName, parameters);
            }

            cmd.Connection = con;

            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// Responsible for deleting a specific Skill record from the database.
        /// </summary>
        /// <param name="SkillID">SkillID of the specific skill.</param>
        /// <returns>result success/ failure</returns>
        public int DeleteSkill(int SkillID)
        {
            var result = 0;

            var parameters = new Dictionary<string, object>
            {
                { "SID", SkillID }
            };

            var con = GetDatabaseConnection();
            var cmd = GetDatabaseCommand(CommandType.StoredProcedure, ProcedureNames.DeleteSkill, parameters);
            cmd.Connection = con;

            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return result;
        }
    }
}
