using Autofac;
using Autofac.Integration.WebApi;
using EmployeeLeave.Core.Repository;
using EmployeeLeave.Core.Services;
using EmployeeLeave.WebAPI.Filters;
using log4net;
using System.Reflection;
using System.Web.Http;

namespace EmployeeLeave.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            #region DI configuration
            // Steps to register Dependenci injection container

            var builder = new ContainerBuilder();

            // Register all dependencies here
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DatabaseRepo>().As<IDatabaseRepo>();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>();
            builder.RegisterType<LeavesService>().As<ILeavesService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.Register(c => LogManager.GetLogger(typeof(object))).As<ILog>();

            // build the container
            var container = builder.Build();

            // set the Dependency resolver of this applciation to Autofac dependency resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion

            // Enable Basic Authentication for entire web application
            config.Filters.Add(new BasicAuthenticationAttribute(container.Resolve<IUserService>()));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
