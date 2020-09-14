using EmployeeLeave.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Core.Services
{
    public interface IUserService
    {
        public User ValidateLogin(string username, string password);
    }
}
