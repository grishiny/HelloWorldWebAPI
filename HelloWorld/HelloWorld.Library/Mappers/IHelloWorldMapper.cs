using HelloWorld.Library.Models;

namespace HelloWorld.Library.Mappers
{
    /// <summary>
    ///     Mapper service for mapping types for the Hello World data service
    /// </summary>
    public interface IHelloWorldMapper
    {
        /// <summary>
        ///     Maps a string to a HelloWorldData model
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>A HelloWorldData model</returns>
        HelloWorldData StringToHelloWorldData(string input);
    }
}