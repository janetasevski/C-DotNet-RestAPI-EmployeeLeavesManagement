using EmployeeLeave.Core.Models;
using FluentValidation;

namespace EmployeeLeave.Core.Validation
{
    public class Employee_Validation : AbstractValidator<Employee>
    {
        public Employee_Validation()
        {
            RuleFor(x => x.First_name).NotEmpty().WithMessage("Employee name is required!");
            RuleFor(x => x.Last_name).NotEmpty().WithMessage("Employee last name is required!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Employee email name is required!");
        }
    }
}
