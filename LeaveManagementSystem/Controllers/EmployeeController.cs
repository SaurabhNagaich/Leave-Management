using System.Text.RegularExpressions;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
   
        readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #region AddLeaveRequest
        [HttpGet]
        public ActionResult AddLeaveRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddLeaveRequest(Leave leave)
        {
            _employeeRepository.AddLeaveRequest(leave);
            return RedirectToAction("AddLeaveRequest");
        }
        #endregion

        #region GetAllLeave
        [HttpGet]
        public ActionResult GetAllLeave(LeaveSearchModel leavesearch)
        {
            if(leavesearch.leaveDateStartFrom == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                leavesearch.leaveDateStartFrom = DateTime.Now;
                leavesearch.LeaveDateEnd = DateTime.Now.AddDays(7);
            }
            List<Leave> allLeaves = _employeeRepository.GetAllLeave(leavesearch);
            return View(allLeaves);
        }
        #endregion

        #region GetAllLeaveForOneMonth
        [HttpGet]
        public ActionResult GetAllLeaveForOneMonth(LeaveSearchModel leavesearch)
        {
            if (leavesearch.leaveDateStartFrom == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                leavesearch.leaveDateStartFrom = DateTime.Now;
                leavesearch.LeaveDateEnd = DateTime.Now.AddDays(30);
            }
            List<Leave> allLeaves = _employeeRepository.GetAllLeave(leavesearch);
            return View(allLeaves);
        }
        #endregion

        #region DeleteLeave
        public ActionResult Delete(int Id)
        {
            _employeeRepository.DeleteLeave(Id);
            return RedirectToAction("GetAllLeave");
        }
        #endregion

        #region EditLeave
        [HttpGet]
        public ActionResult EditLeave(int id)
        {
            Leave leave = _employeeRepository.GetLeaveByLeaveId(id);
            return View(leave);

        }
        [HttpPost]
        public ActionResult EditLeave(Leave leave)
        {
            _employeeRepository.EditLeave(leave);
            return RedirectToAction("GetAllLeave");
        }
        #endregion

    }
}
