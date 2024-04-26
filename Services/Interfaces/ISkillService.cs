using HCM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HCM.Services.Interfaces
{
    public interface ISkillService
    {
        IList<SkillModel> GetAllSkills();
        public int SaveSkill(SkillModel skill);
        public int DeleteSkill(int roleID);
    }
}