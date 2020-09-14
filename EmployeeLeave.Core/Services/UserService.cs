using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Repository;

namespace EmployeeLeave.Core.Services
{
    public class UserService : IUserService
    {
        IDatabaseRepo repo;
        public UserService(IDatabaseRepo repo)
        {
            this.repo = repo;
        }

        public User ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
            return repo.GetUser(username, password);
        }
    }
}
