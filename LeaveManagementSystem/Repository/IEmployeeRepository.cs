using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Repository
{
    public interface IEmployeeRepository
    {
        bool AddLeaveRequest(Leave leave);
        bool DeleteLeave(int leaveId);
        bool EditLeave(Leave leave);
        List<Leave> GetAllLeave(LeaveSearchModel leavesearch);
        Leave GetLeaveByLeaveId(int leaveId);
    }
}
