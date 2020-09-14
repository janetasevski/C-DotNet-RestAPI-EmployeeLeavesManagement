using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeLeave.WebAPI.Controllers
{
    [Authorize]
    public class LeavesController : ApiController
    {
        private readonly ILeavesService leaveServ;
        public LeavesController(ILeavesService leaveServ)
        {
            this.leaveServ = leaveServ;
        }

        //Create new leave for employee
        [Route("api/leaves")]
        [HttpPost]
        public HttpResponseMessage AddLeave(int employee_id, int days, DateTime start_date)
        {
            LeavesTotal lt = leaveServ.GetTotalLeavesForYearByEmployeeId(employee_id, start_date.Year);
            if(lt != null)
                if (days > lt.Left_days) return Request.CreateResponse(HttpStatusCode.BadRequest, "Leave not processed. You reach max days in year!");

            if (leaveServ.AddLeave(employee_id, days, start_date))
                return Request.CreateResponse(HttpStatusCode.Created);
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        //Get all leaves for employee
        [Route("api/leaves/employee/{employee_id}")]
        [HttpGet]
        public List<Leaves> GetAllLeavesByEmployeeId(int employee_id)
        {
            return leaveServ.GetAllLeavesByEmployeeId(employee_id);
        }

        //Get total leave details in year for employee
        [Route("api/leaves/employee/{employee_id}/year/{year}")]
        [HttpGet]
        public IHttpActionResult GetTotalLeavesForYearByEmployeeId(int employee_id, int year)
        {
            LeavesTotal lt = leaveServ.GetTotalLeavesForYearByEmployeeId(employee_id, year);
            if (lt != null)
                return Ok(lt);
            else
                return NotFound();
        }

    }
}
