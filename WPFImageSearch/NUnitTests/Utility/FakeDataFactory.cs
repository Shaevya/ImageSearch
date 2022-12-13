using ImageSearchServer.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Utility
{
    public class FakeDataFactory
    {
        private const string COMMON_EXCEPTION_MESSAGE = "Exception caught in the method";
        public ImageInfoModel GetFakeImageInfoData()
        {
            return new ImageInfoModel()
            {
                CurrentPage = 1,
                TotalPages = 1,
                ItemsPerPage = 1,
                TotalResults = 1,
                Images = GetFakeImages()
            };
        }

        public List<ImageModel> GetFakeImages()
        {
            List<ImageModel> images = new List<ImageModel>();
            var image = new ImageModel()
            {
                Id = "1",
                Owner = "1",
                Secret = "1",
                Server = "1",
                Farm = 1,
                Title = "",
                IsPublic = 1,
                IsFriend = 1,
                IsFamily = 1
            };
            image.URL = $"http://farm{image.Farm}.staticflickr.com/{image.Server}/{image.Id}_{image.Secret}.jpg";

            images.Add(image);

            return images;
        }

        public string GetExceptionMessage()
        {
            return COMMON_EXCEPTION_MESSAGE;
        }

        public string GetFakeImagesResponseDataFromHttpClient()
        {
            string response = "{\"photos\":{\"page\":1,\"pages\":1,\"perpage\":1,\"total\":1,\"photo\":[{\"id\":\"1\",\"owner\":\"1\",\"secret\":\"1\",\"server\":\"1\",\"farm\":1,\"title\":\"\",\"ispublic\":1,\"isfriend\":1,\"isfamily\":1}]},\"stat\":\"ok\"}";
            return response;
        }

    }
}
