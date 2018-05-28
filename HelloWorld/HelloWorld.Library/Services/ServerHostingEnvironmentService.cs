using System.Web.Hosting;

namespace HelloWorld.Library.Services
{
    /// <summary>
    ///     Service for Server Hosting Environment
    /// </summary>
    public class ServerHostingEnvironmentService : IHostingEnvironmentService
    {
        /// <summary>
        ///     Map's the specified path to the hosting environment's path
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The hosting environment's path</returns>
        public string MapPath(string path)
        {
            return HostingEnvironment.MapPath("~/" + path);
        }
    }
}