using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCM.Models
{
    /// <summary>
    /// EmployeePTO model object
    /// </summary>
    public class PTOModel
    {
        public int LeaveBalance { get; set; }
        public int PTOTypeID { get; set; }
        public string PTOName { get; set; }
        public int ManagerID { get; set; }
        public string ManagerName {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int NumDays {  get; set; }
        public string Reason {  get; set; }
        public List<SelectListItem> AllPTOTypes { get;  set; }

        public PTOModel()
        {
            AllPTOTypes = new List<SelectListItem>();
        }
    }

}
