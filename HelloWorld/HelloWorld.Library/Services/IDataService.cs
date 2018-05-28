using HelloWorld.Library.Models;

namespace HelloWorld.Library.Services
{
    /// <summary>
    ///     Data Service for manipulating data
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        ///     Gets hello world data
        /// </summary>
        /// <returns>A HelloWorldData model</returns>
        HelloWorldData GetHelloWorldData();
    }
}