using System;
using System.Collections.Generic;
using HelloWorld.ConsoleApp.Application;
using HelloWorld.ConsoleApp.Services;
using HelloWorld.Library.Models;
using HelloWorld.Library.Services;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace HelloWorld.API.UnitTest
{
    /// <summary>
    ///     Unit tests for the Hello World Console App
    /// </summary>
    [TestFixture]
    public class HelloWorldConsoleAppUnitTests
    {
        /// <summary>
        ///     The list of log messages set by calling classes
        /// </summary>
        private List<string> logMessageList;

        /// <summary>
        ///     The list of exceptions set by calling classes
        /// </summary>
        private List<Exception> exceptionList;

        /// <summary>
        ///     The list of other properties set by calling classes
        /// </summary>
        private List<object> otherPropertiesList;

        /// <summary>
        ///     The mocked Hello World Web Service
        /// </summary>
        private Mock<IHelloWorldWebService> helloWorldWebServiceMock;

        /// <summary>
        ///     The test logger
        /// </summary>
        private ILogger testLogger;

        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldConsoleApp helloWorldConsoleApp;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.helloWorldWebServiceMock = new Mock<IHelloWorldWebService>();
            this.testLogger = new TestLogger(ref logMessageList, ref exceptionList, ref otherPropertiesList);

            // Create object to test
            this.helloWorldConsoleApp = new HelloWorldConsoleApp(this.helloWorldWebServiceMock.Object, testLogger);
        }

        /// <summary>
        ///     Test tear down. (runs after each test)
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clear lists
            this.logMessageList.Clear();
            this.exceptionList.Clear();
            this.otherPropertiesList.Clear();
        }

        #region Run Tests
        /// <summary>
        ///     Tests the class's Run method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!";

            // Create return models for dependencies
            var helloWorldData = GetSampleHelloWorldData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetHelloWorldData()).Returns(helloWorldData);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }

        /// <summary>
        ///     Tests the class's Run method for success when null data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetHelloWorldData()).Returns((HelloWorldData)null);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
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
