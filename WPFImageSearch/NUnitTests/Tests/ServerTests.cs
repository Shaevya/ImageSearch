using ImageSearchServer.Core;
using ImageSearchServer.MVVM.Model;
using ImageSearchServer.Contracts;
using ImageSearchServer.DataAccesslayer;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnitTests.Utility;
using SharedUtility;
using Serilog;

namespace NUnitTests.Tests
{
    [TestFixture]
    public class ServerTests : FakeDataFactory
    {
        private Mock<ICustomHttpClientFactory> _HttpClientFactory;
        private Mock<IHttpRequestUtility> _HttpClientServerUtility;
        private Mock<IConfiguration> _configuration;
        private Mock<ILogger> _logger;

        [SetUp]
        public void MockLayers()
        {
            _HttpClientFactory = new Mock<SharedUtility.ICustomHttpClientFactory>();
            _HttpClientServerUtility = new Mock<ImageSearchServer.Contracts.IHttpRequestUtility>();
            _logger = new Mock<ILogger>();
            _configuration = new Mock<IConfiguration>();
        }


        [Test]
        public void Should_Not_Get_Images_When_Exception_Is_thrown()
        {
            string exceptionMessage = GetExceptionMessage();
            _HttpClientServerUtility.Setup(x => x.GetImages(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromException<ImageInfoModel>(new Exception(exceptionMessage)));
            IApplicationDAL _ApplicationDAL = new ApplicationDAL(_HttpClientServerUtility.Object);

            try
            {
                var images = _ApplicationDAL.GetImages(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()).Result;
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if(ex.InnerException == null)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.That(ex.InnerException.Message, Is.EqualTo(exceptionMessage));
                }
            }
        }
    }
}
