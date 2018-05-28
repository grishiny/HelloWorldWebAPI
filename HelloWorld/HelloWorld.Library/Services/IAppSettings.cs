namespace HelloWorld.Library.Services
{
    /// <summary>
    ///     Service for application settings
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        ///     Gets the string value of a configuration value
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>The application settings value</returns>
        string Get(string name);
    }
}