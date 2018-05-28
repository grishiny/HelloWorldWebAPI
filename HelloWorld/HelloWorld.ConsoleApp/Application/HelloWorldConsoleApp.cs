using HelloWorld.ConsoleApp.Services;
using HelloWorld.Library.Services;

namespace HelloWorld.ConsoleApp.Application
{
    /// <summary>
    ///     Hello World Console Application
    /// </summary>
    public class HelloWorldConsoleApp : IHelloWorldConsoleApp
    {
        /// <summary>
        ///     The Hello World Web Service
        /// </summary>
        private readonly IHelloWorldWebService helloWorldWebService;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldConsoleApp" /> class.
        /// </summary>
        /// <param name="helloWorldWebService">The injected hello world web service</param>
        /// <param name="logger">The logger</param>
        public HelloWorldConsoleApp(IHelloWorldWebService helloWorldWebService, ILogger logger)
        {
            this.helloWorldWebService = helloWorldWebService;
            this.logger = logger;
        }

        /// <summary>
        ///     Runs the main Hello World Console Application
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        public void Run(string[] arguments)
        {
            // Get data
            var helloWorldData = this.helloWorldWebService.GetHelloWorldData();

            // Write data to the screen
            this.logger.Info(helloWorldData != null ? helloWorldData.Data : "No data was found!", null);
        }
    }
}
