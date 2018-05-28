using HelloWorld.ConsoleApp.Application;
using HelloWorld.ConsoleApp.Services;
using HelloWorld.Library.FrameworkWrappers;
using HelloWorld.Library.Services;
using RestSharp;
using LightInject;

namespace HelloWorld.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup dependency injection and run the application
            using (var container = new ServiceContainer())
            {
                // Configure depenency injection
                container.Register<IHelloWorldConsoleApp, HelloWorldConsoleApp>();
                container.Register<IAppSettings, ConfigAppSettings>();
                container.Register<IConsole, SystemConsole>();
                container.Register<ILogger, ConsoleLogger>();
                container.Register<IUri, SystemUri>();
                container.Register<IHelloWorldWebService, HelloWorldWebService>();
                container.RegisterInstance(typeof(IRestClient), new RestClient());
                container.RegisterInstance(typeof(IRestRequest), new RestRequest());

                // Run the main program
                container.GetInstance<IHelloWorldConsoleApp>().Run(args);
            }
        }
    }
}
