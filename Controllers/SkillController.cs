using HCM.Models;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HCM.Controllers
{
    [Authorize]
    public class SkillController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISkillService _SkillService;

        public SkillController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<HomeController>>();
            _SkillService = serviceProvider.GetRequiredService<ISkillService>();
        }

        public IActionResult Skills()
        {
            return View();
        }

        public PartialViewResult AddSkill()
        {
            return new PartialViewResult
            {
                ViewName = "AddSkill",
                ViewData = new ViewDataDictionary<SkillModel>(ViewData, new SkillModel())
            };
        }

        public PartialViewResult EditSkill(SkillModel skill)
        {
            return new PartialViewResult
            {
                ViewName = "EditSkill",
                ViewData = new ViewDataDictionary<SkillModel>(ViewData, skill)
            };
        }

        [HttpGet]
        public JsonResult GetAllSkills()
        {
            return Json(_SkillService.GetAllSkills());
        }

        [HttpPost]
        public JsonResult SaveSkill(SkillModel skill)
        {
            
            return Json(_SkillService.SaveSkill(skill));
        }

        [HttpPost]
        public JsonResult DeleteSkill([FromQuery] int SkillID)
        {
            return Json(_SkillService.DeleteSkill(SkillID));
        }
    }
}
