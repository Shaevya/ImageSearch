using ImageSearchServer.MVVM.Model;
using ImageSearchServer.Contracts;
using System.Text.Json;
using Serilog;
using ImageSearchServer.DataAccesslayer.Utility;
using ILogger = Serilog.ILogger;

namespace ImageSearchServer.Core
{
    public class HttpRequestUtility : IHttpRequestUtility
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        //private readonly string API_KEY = "4b82454906517f04e5b36d920c38fe0e";
        private readonly string API_KEY;
        private readonly string GET_IMAGES_URL; 
        private readonly string IMAGE_URL;

        public HttpRequestUtility(SharedUtility.ICustomHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger logger)
        {
            _httpClient = httpClientFactory.Create();
            _configuration = configuration;
            _logger = logger;
            API_KEY = _configuration[ServerConstants.AppSettingsKey.FlickrAPIKey];
            GET_IMAGES_URL = _configuration[ServerConstants.AppSettingsKey.GetImagesURL];
            IMAGE_URL = _configuration[ServerConstants.AppSettingsKey.ImageURL];
        }

        public async Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            try
            {
                //string URL = $"https://www.flickr.com/services/rest/?method=flickr.photos.search&api_key={API_KEY}&tags={keyword}&per_page={itemsPerPage}&page={pageNo}&format=json&nojsoncallback=1";
                string URL = GET_IMAGES_URL.Replace("{API_KEY}", API_KEY)
                    .Replace("{keyword}", keyword)
                    .Replace("{itemsPerPage}", itemsPerPage.ToString())
                    .Replace("{pageNo}", pageNo.ToString());

                var response = _httpClient.GetStringAsync(URL).GetAwaiter().GetResult();

                var data = JsonSerializer.Deserialize<ImagesHttpResponseModel>(response) ?? new ImagesHttpResponseModel();

                AddImageURL(data.ImagesInfo.Images);

                return data.ImagesInfo;
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in HttpRequestUtility.GetImages, ExMsg: {ex.ToString()}");
                throw;
            }

        }

        private void AddImageURL(List<ImageModel> images)
        {
            try
            {
                foreach (var image in images)
                {
                    try
                    {
                        //image.URL = $"http://farm{image.Farm}.staticflickr.com/{image.Server}/{image.Id}_{image.Secret}.jpg";
                        image.URL = IMAGE_URL.Replace("{image.Farm}", image.Farm.ToString())
                            .Replace("{image.Server}", image.Server)
                            .Replace("{image.Id}", image.Id.ToString())
                            .Replace("{image.Secret}", image.Secret);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning($"Exception in frmin url for image : , ExMsg: {image.Title}");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in HttpRequestUtility.GetImages, ExMsg: {ex.ToString()}");
                throw;
            }
        }
    }
}
