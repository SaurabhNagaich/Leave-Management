using System.Data;
using System.Data.SqlClient;
using LeaveManagementSystem.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;

namespace LeaveManagementSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;

        public static int leaveIdToBeEdited= 0;
        public bool AddLeaveRequest(Leave leave)
        {
            conn = new SqlConnection("Server =BPLDEVDB01; Database =SNagaich_Credit_CoreLibrary;Trusted_Connection =True;");
            cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WeeklyLeaveViewer";
            cmd.Parameters.AddWithValue("@Name", leave.Name);
            cmd.Parameters.AddWithValue("@LeaveStartDate", leave.LeaveStartDate);
            cmd.Parameters.AddWithValue("@LeaveEndDate", leave.LeaveEndDate);
            cmd.Parameters.AddWithValue("@ReasonForLeave", leave.ReasonForLeave);
            cmd.Parameters.AddWithValue("@LeaveType", leave.LeaveType);
            cmd.Parameters.AddWithValue("@StatementType", "InsertLeaveRequest");
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
                return false;
            }
            return true;
        }

        public bool DeleteLeave(int leaveId)
        {
            conn = new SqlConnection("Server =BPLDEVDB01; Database =SNagaich_Credit_CoreLibrary;Trusted_Connection =True;");
            cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WeeklyLeaveViewer";
            cmd.Parameters.AddWithValue("@LeaveId", leaveId);
            cmd.Parameters.AddWithValue("@StatementType", "DeleteLeave");
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
                return false;
            }
            return true;
        }

        public bool EditLeave(Leave leave)
        {
            leave.LeaveId = leaveIdToBeEdited;
            conn = new SqlConnection("Server =BPLDEVDB01; Database =SNagaich_Credit_CoreLibrary;Trusted_Connection =True;");
            cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WeeklyLeaveViewer";
            cmd.Parameters.AddWithValue("@Name", leave.Name);
            cmd.Parameters.AddWithValue("@LeaveDate", leave.LeaveDate);
            cmd.Parameters.AddWithValue("@LeaveType", leave.LeaveType);
            cmd.Parameters.AddWithValue("@LeaveId", leave.LeaveId);
            cmd.Parameters.AddWithValue("@ReasonForLeave", leave.ReasonForLeave);
            cmd.Parameters.AddWithValue("@StatementType", "EditLeaveRequest");
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                conn.Close();
                conn.Dispose();
                return false;
            }
            return true;
        }

        public List<Leave> GetAllLeave(LeaveSearchModel leavesearch)
        {
            conn = new SqlConnection("Server =BPLDEVDB01; Database =SNagaich_Credit_CoreLibrary;Trusted_Connection =True;");
            cmd = new SqlCommand();
            List<Leave> allLeaveList = new List<Leave>();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WeeklyLeaveViewer";
            cmd.Parameters.AddWithValue("@leaveDateStartFrom", leavesearch.leaveDateStartFrom);
            cmd.Parameters.AddWithValue("@LeaveDateEnd", leavesearch.LeaveDateEnd);
            cmd.Parameters.AddWithValue("@StatementType", "GetAllLeaveFromCurrentDate");
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Leave leave = new Leave();
                leave.LeaveId = (int)reader["LeaveId"];
                leave.Name = (string)reader["Name"];
                leave.LeaveType = (string)reader["LeaveType"];
                leave.LeaveDate = (DateTime)reader["LeaveDate"];
                allLeaveList.Add(leave);
            }
            conn.Close();
            return allLeaveList;
        }

        public Leave GetLeaveByLeaveId(int leaveId)
        {
            leaveIdToBeEdited= leaveId;
            conn = new SqlConnection("Server =BPLDEVDB01; Database =SNagaich_Credit_CoreLibrary;Trusted_Connection =True;");
            cmd = new SqlCommand();
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_WeeklyLeaveViewer";
            cmd.Parameters.AddWithValue("@LeaveId", leaveId);
            cmd.Parameters.AddWithValue("@StatementType","GetLeaveByLeaveId");
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Leave leave = new Leave();
            while (reader.Read())
            {
                leave.LeaveId = (int)reader["LeaveId"];
                leave.Name = (string)reader["Name"];
                leave.LeaveType = (string)reader["LeaveType"];
                leave.LeaveDate = (DateTime)reader["LeaveDate"];
                leave.ReasonForLeave = (string)reader["ReasonForLeave"];
            }
            conn.Close();
            return leave;
        }
    }
}
