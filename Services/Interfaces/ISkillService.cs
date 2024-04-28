using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface ISkillService
    {
        /// <summary>
        /// Service Contract for all the REST API calls for the Skill module views and functionalities.
        /// </summary>
        IList<SkillModel> GetAllSkills();
        public int SaveSkill(SkillModel skill);
        public int DeleteSkill(int roleID);
    }
}