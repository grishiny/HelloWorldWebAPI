using System.Configuration;
using HelloWorld.Library.FrameworkWrappers;
using HelloWorld.Library.Mappers;
using HelloWorld.Library.Models;
using HelloWorld.Library.Resources;

namespace HelloWorld.Library.Services
{
    /// <summary>
    ///     Data service for manipulating Hello World data
    /// </summary>
    public class HelloWorldDataService : IDataService
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly IAppSettings appSettings;

        /// <summary>
        ///     The DateTime wrapper
        /// </summary>
        private readonly IDateTime dateTimeWrapper;

        /// <summary>
        ///     The File IO service
        /// </summary>
        private readonly IFileIOService fileIOService;

        /// <summary>
        ///     The Hello World Mapper
        /// </summary>
        private readonly IHelloWorldMapper helloWorldMapper;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldDataService" /> class.
        /// </summary>
        /// <param name="appSettings">The injected application settings service</param>
        /// <param name="dateTimeWrapper">The injected DateTime wrapper</param>
        /// <param name="fileIOService">The injected File IO Service</param>
        /// <param name="helloWorldMapper">The injected Hello World Mapper</param>
        public HelloWorldDataService(
            IAppSettings appSettings,
            IDateTime dateTimeWrapper,
            IFileIOService fileIOService,
            IHelloWorldMapper helloWorldMapper)
        {
            this.appSettings = appSettings;
            this.dateTimeWrapper = dateTimeWrapper;
            this.fileIOService = fileIOService;
            this.helloWorldMapper = helloWorldMapper;
        }

        /// <summary>
        ///     Gets hello world data
        /// </summary>
        /// <returns>A HelloWorldData model</returns>
        public HelloWorldData GetHelloWorldData()
        {
            // Get the file path
            var filePath = this.appSettings.Get(AppSettingsKeys.HelloWorldDataFileKey);

            if (string.IsNullOrEmpty(filePath))
            {
                // No file path was found, throw exception
                throw new SettingsPropertyNotFoundException(
                    ErrorCodes.HelloWorldDataFileSettingsKeyError, 
                    new SettingsPropertyNotFoundException("The HelloWorldDataFile settings key was not found or had no value."));
            }

            // Get the data from the file
            var rawData = this.fileIOService.ReadFile(filePath);

            // Add the timestamp
            rawData += " at " + this.dateTimeWrapper.Now().ToString("F");

            // Map to the return type
            var helloWorldData = this.helloWorldMapper.StringToHelloWorldData(rawData);

            return helloWorldData;
        }
    }
}