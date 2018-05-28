using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.API.Controllers;
using HelloWorld.Library.Models;
using HelloWorld.Library.Services;
using Moq;
using NUnit.Framework;

namespace HelloWorld.API.UnitTest
{
    /// <summary>
    ///     Unit tests for the HelloWorld Controller
    /// </summary>
    [TestFixture]
    public class HelloWorldControllerUnitTests
    {
        /// <summary>
        ///     The mocked data service
        /// </summary>
        private Mock<IDataService> dataServiceMock;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldController helloWorldController;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Setup mocked dependencies
            this.dataServiceMock = new Mock<IDataService>();

            // Create object to test
            this.helloWorldController = new HelloWorldController(this.dataServiceMock.Object);
        }

        #region Get Tests
        /// <summary>
        ///     Tests the controller's get method for success
        /// </summary>
        [Test]
        public void UnitTestHelloWorldControllerGetSuccess()
        {
            // Create the expected result
            var expectedResult = GetSampleHelloWorldData();

            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetHelloWorldData()).Returns(expectedResult);

            // Call the method to test
            var result = this.helloWorldController.Get();

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        /// <summary>
        ///     Tests the controller's get method for a SettingsPropertyNotFoundException
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(SettingsPropertyNotFoundException))]
        public void UnitTestHelloWorldControllerGetSettingsPropertyNotFoundException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetHelloWorldData()).Throws(new SettingsPropertyNotFoundException("Error!"));

            // Call the method to test
            this.helloWorldController.Get();
        }

        /// <summary>
        ///     Tests the controller's get method for an IOException
        /// </summary>
        [Test]
        [ExpectedException(ExpectedException = typeof(IOException))]
        public void UnitTestHelloWorldControllerGetIOException()
        {
            // Set up dependencies
            this.dataServiceMock.Setup(m => m.GetHelloWorldData()).Throws(new IOException("Error!"));

            // Call the method to test
            this.helloWorldController.Get();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a sample HelloWorldData model
        /// </summary>
        /// <returns>A sample HelloWorldData model</returns>
        private static HelloWorldData GetSampleHelloWorldData()
        {
            return new HelloWorldData()
            {
                Data = "Hello World!"
            };
        }
        #endregion
    }
}
