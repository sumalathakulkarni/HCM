using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    /// <summary>
    /// Controller for all the views under the Skill module.
    /// </summary>
    /// The below tag ensures that only the authenticated user has access to the views under the Skill module.
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISkillService _SkillService; //Skill Service instance declaration.

        public SkillController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();

            //Skill Service instance initialization.
            _SkillService = serviceProvider.GetRequiredService<ISkillService>();
        }

        /// <summary>
        /// Responsible for displaying the Skills list View.
        /// </summary>
        /// <returns>View => Skills list json</returns>
        public IActionResult Skills()
        {
            return View();
        }

        /// <summary>
        /// Responsible for displaying the Add New Skill pop-up box.
        /// </summary>
        /// <returns>PartialViewResult => Add New Skill json</returns>
        public PartialViewResult AddSkill()
        {
            return new PartialViewResult
            {
                ViewName = "AddSkill",
                ViewData = new ViewDataDictionary<SkillModel>(ViewData, new SkillModel())
            };
        }

        /// <summary>
        /// Responsible for displaying the Edit Skill pop-up box (Selected skill from the skills list).
        /// </summary>
        /// <param name="skill">SkillModel object with field values set from the input fields.</param>
        /// <returns>PartialViewResult => Edit Skill json</returns>
        public PartialViewResult EditSkill(SkillModel skill)
        {
            return new PartialViewResult
            {
                ViewName = "EditSkill",
                ViewData = new ViewDataDictionary<SkillModel>(ViewData, skill)
            };
        }

        /// <summary>
        /// REST API call to retrieve (GET) the list of all skills.
        /// </summary>
        /// <returns>Skills list json</returns>
        [HttpGet]
        public JsonResult GetAllSkills()
        {
            return Json(_SkillService.GetAllSkills());
        }

        /// <summary>
        /// REST API call to submit the skill details for Add New Skill/ Edit Skill functionalities.
        /// </summary>
        /// <param name="skill">SkillModel with the fields set with the values from the input fields in the UI</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult SaveSkill(SkillModel skill)
        {
            
            return Json(_SkillService.SaveSkill(skill));
        }

        /// <summary>
        /// REST API call to delete the selected skill (POST) from the list of skills.
        /// </summary>
        /// <param name="SkillID">SkillID of the selected skill.</param>
        /// <returns>json success/ failure</returns>
        [HttpPost]
        public JsonResult DeleteSkill([FromQuery] int SkillID)
        {
            return Json(_SkillService.DeleteSkill(SkillID));
        }
    }
}
