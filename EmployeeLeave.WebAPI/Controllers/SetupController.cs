using EmployeeLeave.Core.Models;
using EmployeeLeave.Core.Services;
using System.Web.Http;

namespace EmployeeLeave.WebAPI.Controllers
{
    [Authorize]
    public class SetupController : ApiController
    {
        private readonly ILeavesService leaveServ;
        public SetupController(ILeavesService leaveServ)
        {
            this.leaveServ = leaveServ;
        }

        //Get maximum leave days in year
        [Route("api/setup/getmaxdays")]
        [HttpGet]
        public IHttpActionResult GetMaxDays()
        {
            Setup max_days = leaveServ.GetMaxDays();
            if (max_days != null)
                return Ok(max_days);
            else
                return NotFound();
        }

        //Set maximum leave days in year
        [Authorize(Roles = "admin")]
        [Route("api/setup/setmaxdays")]
        [HttpPut]
        public IHttpActionResult SetMaxDays(int days)
        {
            if (leaveServ.SetMaxDays(days))
                return Ok();
            else
                return NotFound();
        }
    }
}
