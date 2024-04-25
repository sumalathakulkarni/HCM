using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCM.Models
{
    public class SkillsModel
    {
        public List<SelectListItem> AllSkills { get; set; }
        public List<SelectListItem> EmployeeSkills { get; set; }
        public SkillsModel()
        {
            AllSkills = new List<SelectListItem>();
            EmployeeSkills = new List<SelectListItem>();
        }
    }
}
