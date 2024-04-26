using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCM.Models
{
    public class EmpSkillsModel
    {
        public List<SelectListItem> AllSkills { get; set; }
        public List<SelectListItem> EmployeeSkills { get; set; }
        public EmpSkillsModel()
        {
            AllSkills = new List<SelectListItem>();
            EmployeeSkills = new List<SelectListItem>();
        }
    }
}
