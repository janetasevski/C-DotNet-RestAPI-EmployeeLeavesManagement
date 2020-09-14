using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Repository;
using EmployeeLeave.Core.Validation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace EmployeeLeave.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        IDatabaseRepo repo;
        public EmployeeService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }
        public bool AddEmployee(Employee emp)
        {
            if (emp == null) return false;
            return repo.AddEmployee(emp);
        }

        public bool DeleteEmployee(int id)
        {
            return repo.DeleteEmployee(id);
        }

        public List<Employee> GetAllEmployees()
        {
            return repo.GetAllEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return repo.GetEmployeeById(id);
        }

        public bool UpdateEmployee(int id, Employee emp)
        {
            if (emp == null) return false;
            return repo.UpdateEmployee(id, emp);
        }

        public ValidationResult ValidateEmployee(Employee employee)
        {
            if (employee == null) return null;

            Employee_Validation empValidation = new Employee_Validation();
            return empValidation.Validate(employee);
        }
    }
}
