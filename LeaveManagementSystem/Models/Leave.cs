using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models
{
    public class Leave
    {
        public int LeaveId { get; set; }
        public string Name { get; set; }

        [Display(Name= "Leave Start Date")]
        public DateTime LeaveStartDate { get; set; }

        [Display(Name = "Leave End Date")]
        public DateTime LeaveEndDate { get; set;}

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }
        public int NoOfDays { get; set; }
        public string ReasonForLeave { get; set; }

        [Display(Name = "Leave Date")]
        public DateTime LeaveDate { get; set; }

    }
}
