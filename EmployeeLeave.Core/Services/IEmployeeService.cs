using EmployeeLeave.Core.Models;
using System.Collections.Generic;

namespace EmployeeLeave.Core.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        bool AddEmployee(Employee emp);
        bool UpdateEmployee(int id, Employee emp);
        bool DeleteEmployee(int id);
    }
}
