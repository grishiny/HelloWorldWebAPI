using NUnit.Framework;
using HelloWorld.Library.Mappers;
using HelloWorld.Library.Models;

namespace HelloWorld.API.UnitTest
{
    /// <summary>
    ///     Unit tests for the Hello World Mapper
    /// </summary>
    [TestFixture]
    public class HelloWorldMapperUnitTests
    {
        /// <summary>
        ///     The implementation to test
        /// </summary>
        private HelloWorldMapper helloWorldMapper;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Create object to test
            this.helloWorldMapper = new HelloWorldMapper();
        }

        #region StringToHelloWorldData Tests
        /// <summary>
        ///     Tests the class's StringToHelloWorldData method for success with a normal input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperStringToHelloWorldDataNormalSuccess()
        {
            const string Data = "Hello World!";

            // Create the expected result
            var expectedResult = GetSampleHelloWorldData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToHelloWorldData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
        }

        /// <summary>
        ///     Tests the StringToHelloWorldData method for success with a null input value
        /// </summary>
        [Test]
        public void UnitTestHelloWorldMapperStringToHelloWorldDataNullSuccess()
        {
            const string Data = null;

            // Create the expected result
            var expectedResult = GetSampleHelloWorldData(Data);

            // Call the method to test
            var result = this.helloWorldMapper.StringToHelloWorldData(Data);

            // Check values
            Assert.NotNull(result);
            Assert.AreEqual(result.Data, expectedResult.Data);
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
            return new HelloWorldData()
            {
                Data = data
            };
        }
        #endregion
    }
}
