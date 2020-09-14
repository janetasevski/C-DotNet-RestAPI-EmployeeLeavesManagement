using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Services;
using EmployeeLeave.Core.Validation;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeLeave.WebAPI.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeService empServ;
        public EmployeesController(IEmployeeService empServ)
        {
            this.empServ = empServ;
        }

        [Route("api/employees")]
        [HttpGet]
        public List<Employee> GetAllEmployees()
        {
            return empServ.GetAllEmployees();
        }

        [Route("api/employees/{id}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeById(int id)
        {
            Employee emp = empServ.GetEmployeeById(id);
            if (emp != null)
                return Ok(emp);
            else
                return NotFound();
        }

        [Route("api/employees")]
        [HttpPost]
        public HttpResponseMessage AddEmployee(Employee emp)
        {

            Employee_Validation validation = new Employee_Validation();
            var result = validation.Validate(emp);
            if (!result.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Validation not pass");

            if (empServ.AddEmployee(emp))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

        }

        [Route("api/employees/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateEmployee(int id, Employee emp)
        {
            if (emp == null) return Request.CreateResponse(HttpStatusCode.InternalServerError);
            Employee_Validation validation = new Employee_Validation();
            var result = validation.Validate(emp);
            if (!result.IsValid) return Request.CreateResponse(HttpStatusCode.BadRequest, "Validation not pass");
            if (empServ.UpdateEmployee(id, emp))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/employees/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            if (empServ.DeleteEmployee(id))
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound);
        }

    }
}
