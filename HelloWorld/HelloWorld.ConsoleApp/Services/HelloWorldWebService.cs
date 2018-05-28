using System;
using HelloWorld.Library.FrameworkWrappers;
using HelloWorld.Library.Models;
using HelloWorld.Library.Resources;
using HelloWorld.Library.Services;
using RestSharp;

namespace HelloWorld.ConsoleApp.Services
{
    /// <summary>
    ///     Service class for communicating with the Hello World Web API
    /// </summary>
    public class HelloWorldWebService : IHelloWorldWebService
    {
        /// <summary>
        ///     The application settings service
        /// </summary>
        private readonly IAppSettings appSettings;

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        ///     The Rest client
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        ///     The Rest request
        /// </summary>
        private readonly IRestRequest restRequest;

        /// <summary>
        ///     The wrapped Uri service
        /// </summary>
        private readonly IUri uriService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelloWorldWebService" /> class.
        /// </summary>
        /// <param name="restClient">The rest client</param>
        /// <param name="restRequest">The rest request</param>
        /// <param name="appSettings">The application settings</param>
        /// <param name="uriService">The uri service</param>
        /// <param name="logger">The logger</param>
        public HelloWorldWebService(
            IRestClient restClient,
            IRestRequest restRequest,
            IAppSettings appSettings,
            IUri uriService,
            ILogger logger)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
            this.appSettings = appSettings;
            this.uriService = uriService;
            this.logger = logger;
        }

        /// <summary>
        ///     Gets data from the web API
        /// </summary>
        /// <returns>A HelloWorldData model</returns>
        public HelloWorldData GetHelloWorldData()
        {
            HelloWorldData helloWorldData = null;

            // Set the URL for the request
            this.restClient.BaseUrl = this.uriService.GetUri(this.appSettings.Get(AppSettingsKeys.HelloWorldApiUrlKey));

            // Setup the request
            this.restRequest.Resource = "helloworld";
            this.restRequest.Method = Method.GET;

            // Clear the request parameters
            this.restRequest.Parameters.Clear();

            // Execute the call and get the response
            var helloWorldDataResponse = this.restClient.Execute<HelloWorldData>(this.restRequest);

            // Check for data in the response
            if (helloWorldDataResponse != null)
            {
                // Check if any actual data was returned
                if (helloWorldDataResponse.Data != null)
                {
                    helloWorldData = helloWorldDataResponse.Data;
                }
                else
                {
                    var errorMessage = "Error in RestSharp, most likely in endpoint URL." + " Error message: "
                                       + helloWorldDataResponse.ErrorMessage + " HTTP Status Code: "
                                       + helloWorldDataResponse.StatusCode + " HTTP Status Description: "
                                       + helloWorldDataResponse.StatusDescription;

                    // Check for existing exception
                    if (helloWorldDataResponse.ErrorMessage != null && helloWorldDataResponse.ErrorException != null)
                    {
                        // Log an informative exception including the RestSharp exception
                        this.logger.Error(errorMessage, null, helloWorldDataResponse.ErrorException);
                    }
                    else
                    {
                        // Log an informative exception including the RestSharp content
                        this.logger.Error(errorMessage, null, new Exception(helloWorldDataResponse.Content));
                    }
                }
            }
            else
            {
                // Log the exception
                const string ErrorMessage =
                    "Did not get any response from the Hello World Web Api for the Method: GET /helloworlddata";

                this.logger.Error(ErrorMessage, null, new Exception(ErrorMessage));
            }

            return helloWorldData;
        }
    }
}
