using System;
using System.Configuration;
using System.IO;
using HelloWorld.Library.FrameworkWrappers;
using HelloWorld.Library.Mappers;
using HelloWorld.Library.Models;
using HelloWorld.Library.Resources;
using HelloWorld.Library.Services;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace HelloWorld.API.UnitTest
{
    /// <summary>
    ///     Unit tests for the Hello World Data Service
    /// </summary>
    [TestFixture]
    public class HelloWorldDataServiceUnitTests
    {
        /// <summary>
        ///     The mocked application settings service
        /// </summary>
        private Mock<IAppSettings> appSettingsMock;

        /// <summary>
        ///     The mocked DateTime wrapper
        /// </summary>
        private Mock<IDateTime> dateTimeWrapperMock;

        /// <summary>
        ///     The mocked File IO service
        /// </summary>
        private Mock<IFileIOService> fileIOServiceMock;

        /// <summary>
        ///     The mocked Hello World Mapper
        /// </summary>
        private Mock<IHelloWorldMapper> helloWorldMapperMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldDataService helloWorldDataService;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.appSettingsMock = new Mock<IAppSettings>();
            this.dateTimeWrapperMock = new Mock<IDateTime>();
            this.fileIOServiceMock = new Mock<IFileIOService>();
            this.helloWorldMapperMock = new Mock<IHelloWorldMapper>();

            // Create object to test
            this.helloWorldDataService = new HelloWorldDataService(
                this.appSettingsMock.Object,
                this.dateTimeWrapperMock.Object,
                this.fileIOServiceMock.Object,
                this.helloWorldMapperMock.Object);
        }

        #region GetHelloWorldData Tests
        /// <summary>
        ///     Tests the class's GetHelloWorld method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldDataServiceGetHelloWorldDataSuccess()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";
            const string FileContents = "Hello World!";
            var nowDate = DateTime.Now;
            var rawData = FileContents + " at " + nowDate.ToString("F");

            // Create the expected result
            var expectedResult = GetSampleHelloWorldData(rawData);

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Returns(FileContents);
            this.dateTimeWrapperMock.Setup(m => m.Now()).Returns(nowDate);
            this.helloWorldMapperMock.Setup(m => m.StringToHelloWorldData(rawData)).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldDataService.GetHelloWorldData();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        /// <summary>
        ///     Tests the class's GetHelloWorld method when the setting key is null
        /// </summary>
        [Test]
        [NUnit.Framework.ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetHelloWorldDataSettingKeyNull()
        {
            // Create return models for dependencies
            const string DataFilePath = null;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldDataFileKey)).Returns(DataFilePath);

            // Call the method to test
            this.helloWorldDataService.GetHelloWorldData();
        }

        /// <summary>
        ///     Tests the class's GetHelloWorldData method when the setting key is an empty string
        /// </summary>
        [Test]
        [NUnit.Framework.ExpectedException(typeof(SettingsPropertyNotFoundException), ExpectedMessage = ErrorCodes.HelloWorldDataFileSettingsKeyError)]
        public void UnitTestHelloWorldDataServiceGetHelloWorldDataSettingKeyEmptyString()
        {
            // Create return models for dependencies
            var dataFilePath = string.Empty;

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldDataFileKey)).Returns(dataFilePath);

            // Call the method to test
            this.helloWorldDataService.GetHelloWorldData();
        }

        /// <summary>
        ///     Tests the class's GetHelloWorldData method for an IO Exception
        /// </summary>
        [Test]
        [NUnit.Framework.ExpectedException(typeof(IOException))]
        public void UnitTestHelloWorldDataServiceGetHelloWorldDataIOException()
        {
            // Create return models for dependencies
            const string DataFilePath = "some/path";

            // Set up dependencies
            this.appSettingsMock.Setup(m => m.Get(AppSettingsKeys.HelloWorldDataFileKey)).Returns(DataFilePath);
            this.fileIOServiceMock.Setup(m => m.ReadFile(DataFilePath)).Throws(new IOException("Error!"));

            // Call the method to test
            this.helloWorldDataService.GetHelloWorldData();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a sample HelloWorldData model
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>A sample HelloWorldData model</returns>
        private static HelloWorldData GetSampleHelloWorldData(string data)
        {
            return new HelloWorldData { Data = data };
        }
        #endregion
    }
}
