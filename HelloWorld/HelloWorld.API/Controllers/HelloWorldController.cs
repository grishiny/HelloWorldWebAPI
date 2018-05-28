using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Http;
using HelloWorld.Library.Atributes;
using HelloWorld.Library.Models;
using HelloWorld.Library.Services;

namespace HelloWorld.API.Controllers
{
    public class HelloWorldController : ApiController
    {
        /// <summary>
        ///     The data service
        /// </summary>
        private readonly IDataService dataService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldController" /> class.
        /// </summary>
        /// <param name="dataService">The injected data service</param>
        public HelloWorldController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        /// <summary>
        ///     Gets hellow world value
        /// </summary>
        /// <returns>A HelloWorldData model</returns>
        [WebApiExceptionFilter(Type = typeof(IOException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        [WebApiExceptionFilter(Type = typeof(SettingsPropertyNotFoundException), Status = HttpStatusCode.ServiceUnavailable, Severity = SeverityCode.Error)]
        public HelloWorldData Get()
        {
            return this.dataService.GetHelloWorldData();
        }
    }
}
