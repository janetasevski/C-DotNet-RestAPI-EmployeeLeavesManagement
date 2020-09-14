using EmployeeLeave.Core.Models;
using System;
using System.Collections.Generic;

namespace EmployeeLeave.Core.Repository
{
    public interface IDatabaseRepo
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        bool AddEmployee(Employee emp);
        bool UpdateEmployee(int id, Employee emp);
        bool DeleteEmployee(int id);
        bool AddLeave(int employee_id, int days, DateTime start_date);
        List<Leaves> GetAllLeavesByEmployeeId(int id);
        LeavesTotal GetTotalLeavesForYearByEmployeeId(int employee_id, int year);
        Setup GetMaxDays();
        bool SetMaxDays(int days);
        public User GetUser(string username, string password);
    }
}
