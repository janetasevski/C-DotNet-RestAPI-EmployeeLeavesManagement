using Dapper;
using EmployeeLeave.Core.DBHelper;
using EmployeeLeave.Core.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmployeeLeave.Core.Repository
{
    public class DatabaseRepo : IDatabaseRepo
    {
        ILog logger;
        public DatabaseRepo(ILog logger)
        {
            this.logger = logger;
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@first_name", emp.First_name);
                    par.Add("@last_name", emp.Last_name);
                    par.Add("@city", emp.City);
                    par.Add("@email", emp.Email);
                    int result = conn.Execute("addEmployee", par, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return false;
            }
        }

        public bool AddLeave(int employee_id, int days, DateTime start_date)
        {
            DateTime end_date = start_date.AddDays(days);
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@employee_id", employee_id);
                    par.Add("@days", days);
                    par.Add("@start_date", start_date);
                    par.Add("@end_date", end_date);
                    int result = conn.Execute("addLeave", par, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return false;
            }
        }

        public bool DeleteEmployee(int id)
        {
            logger.Info("Deleting product " + id);
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@emp_id", id);
                    int result = conn.Execute("deleteEmployee", par, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return false;
            }
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    List<Employee> result = conn.Query<Employee>("getAllEmployees", commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public List<Leaves> GetAllLeavesByEmployeeId(int employee_id)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@emp_id", employee_id);
                    List<Leaves> result = conn.Query<Leaves>("getAllLeaves", par, commandType: CommandType.StoredProcedure).ToList();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@emp_id", id);
                    Employee result = conn.Query<Employee>("getEmployeeById", par, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public Setup GetMaxDays()
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    Setup result = conn.Query<Setup>("getMaxDays", commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public LeavesTotal GetTotalLeavesForYearByEmployeeId(int employee_id, int year)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@emp_id", employee_id);
                    par.Add("@year", year);
                    LeavesTotal result = conn.Query<LeavesTotal>("getTotalLeavesForYearByEmployeeId", par, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public User GetUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@username", username);
                    par.Add("@password", password);
                    User result = conn.Query<User>("getLoginUser", par, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return null;
            }
        }

        public bool SetMaxDays(int days)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@max_days", days);
                    int result = conn.Execute("setMaxDays", par, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return false;
            }
        }

        public bool UpdateEmployee(int id, Employee emp)
        {
            try
            {
                using (SqlConnection conn = DBAccess.GetOpenConnection())
                {
                    DynamicParameters par = new DynamicParameters();
                    par.Add("@emp_id", id);
                    par.Add("@first_name", emp.First_name);
                    par.Add("@last_name", emp.Last_name);
                    par.Add("@city", emp.City);
                    par.Add("@email", emp.Email);
                    int result = conn.Execute("updateEmployee", par, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occure: " + e.Message, e);
                return false;
            }
        }
    }
}
