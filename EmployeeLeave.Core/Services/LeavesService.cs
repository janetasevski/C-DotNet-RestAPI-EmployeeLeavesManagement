using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Repository;
using System;
using System.Collections.Generic;

namespace EmployeeLeave.Core.Services
{
    public class LeavesService : ILeavesService
    {
        IDatabaseRepo repo;
        public LeavesService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        public bool AddLeave(int employee_id, int days, DateTime start_date)
        {
            if (employee_id == 0 || days == 0 || start_date == null) return false;
            return repo.AddLeave(employee_id, days, start_date);
        }

        public List<Leaves> GetAllLeavesByEmployeeId(int employee_id)
        {
            if (employee_id == 0) return null;
            return repo.GetAllLeavesByEmployeeId(employee_id);
        }

        public Setup GetMaxDays()
        {
            return repo.GetMaxDays();
        }

        public LeavesTotal GetTotalLeavesForYearByEmployeeId(int employee_id, int year)
        {
            if (employee_id == 0) return null;
            return repo.GetTotalLeavesForYearByEmployeeId(employee_id, year);
        }

        public bool SetMaxDays(int days)
        {
            if (days < 10) return false;
            return repo.SetMaxDays(days);
        }
    }
}
