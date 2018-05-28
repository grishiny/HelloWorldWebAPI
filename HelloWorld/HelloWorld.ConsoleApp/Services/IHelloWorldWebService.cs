using HelloWorld.Library.Models;

namespace HelloWorld.ConsoleApp.Services
{
    /// <summary>
    ///     Service for communicating with the Hello World Web API
    /// </summary>
    public interface IHelloWorldWebService
    {
        /// <summary>
        ///     Gets data from the web API
        /// </summary>
        /// <returns>A HelloWorldData model</returns>
        HelloWorldData GetHelloWorldData();
    }
}
