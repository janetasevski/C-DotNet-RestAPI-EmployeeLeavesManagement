using System.Web.Http;
using WebActivatorEx;
using EmployeeLeave.WebAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace EmployeeLeave.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "EmployeeLeavesWebAPI");
                })
                .EnableSwaggerUi();
        }
    }
}
