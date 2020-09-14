using EmployeeLeave.Core.Models;
using System;
using System.Collections.Generic;

namespace EmployeeLeave.Core.Services
{
    public interface ILeavesService
    {
        bool AddLeave(int employee_id, int days, DateTime start_date);
        List<Leaves> GetAllLeavesByEmployeeId(int employee_id);
        LeavesTotal GetTotalLeavesForYearByEmployeeId(int employee_id, int year);
        Setup GetMaxDays();
        bool SetMaxDays(int days);

    }
}
