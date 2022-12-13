using ImageSearchServer.Core;
using ImageSearchServer.DataAccesslayer;
using ImageSearchServer.MVVM.Model;
using ImageSearchServer.MVVM.ViewModel;
using Moq;
using NUnit.Framework;
using NUnitTests.Utility;
using Serilog;

namespace NUnitTests.Tests
{
    [TestFixture]
    public class ClientTests : FakeDataFactory
    {
        private Mock<IClientAppDAL> _AppDAL;
        private Mock<IHttpClientUtility> _HttpClientUtility;
        private Mock<ILogger> _Logger;
        private MainViewModel _MainViewModel;

        [SetUp]
        public void MockLayers()
        {
            _Logger = new Mock<ILogger>();
            _HttpClientUtility = new Mock<IHttpClientUtility>();
            _AppDAL = new Mock<IClientAppDAL>();
            _MainViewModel = new MainViewModel(_AppDAL.Object, _Logger.Object);
        }

        [Test]
        public void Should_Get_Images_When_Keyword_Is_Not_Empty()
        {

            ImageInfoModel imageInfo = GetFakeImageInfoData();
            _MainViewModel.Keyword = "Intuit";
            _AppDAL.Setup(x => x.GetImages(_MainViewModel.Keyword, It.IsAny<int>(), It.IsAny<int>()))
                .Returns(imageInfo);

            _MainViewModel.SearchImages();

            Assert.That(_MainViewModel.PageNo == 1);
            Assert.That(_MainViewModel.TotalDataCount == imageInfo.TotalResults);
            CollectionAssert.AreEqual(_MainViewModel.Images, imageInfo.Images);
        }

        [Test]
        public void Should_Not_Get_Images_When_Keyword_Is_Not_Empty()
        {
            _MainViewModel.Keyword = "";

            _MainViewModel.SearchImages();

            Assert.That(_MainViewModel.PageNo == 1);
            Assert.That(_MainViewModel.TotalDataCount == 0);
            CollectionAssert.AreEqual(_MainViewModel.Images, new List<ImageModel>());
        }

        [Test]
        public void Should_Not_Get_Images_When_An_Exception_Is_Thrown()
        {
            string exceptionMessage = GetExceptionMessage();

            _MainViewModel.Keyword = "John Doe";
            _HttpClientUtility.Setup(x => x.GetImages(_MainViewModel.Keyword, It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(Task.FromException<ImageInfoModel>(new Exception(exceptionMessage)));

            _MainViewModel.SearchImages();

            Assert.That(_MainViewModel.PageNo == 1);
            Assert.That(_MainViewModel.TotalDataCount == 0);
            CollectionAssert.AreEqual(_MainViewModel.Images, new List<ImageModel>());
        }


        [Test]
        public void Should_Get_Next_Set_Of_Images_When_Keyword_Is_Not_Empty()
        {

            ImageInfoModel imageInfo = GetFakeImageInfoData();
            _MainViewModel.Keyword = "John Doe";
            _MainViewModel.PageNo = 1;
            _AppDAL.Setup(x => x.GetImages(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(imageInfo);

            _MainViewModel.GetNextImages();

            Assert.That(_MainViewModel.PageNo == 2);
            Assert.That(_MainViewModel.TotalDataCount == imageInfo.TotalResults);
            CollectionAssert.AreEqual(_MainViewModel.Images, imageInfo.Images);
        }

    }
}