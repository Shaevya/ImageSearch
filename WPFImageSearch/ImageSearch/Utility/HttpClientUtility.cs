using ImageSearchServer.MVVM.Model;
using ImageSearchServer.DataAccesslayer;
using Microsoft.Extensions.Configuration;
using Serilog;
using SharedUtility;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImageSearchServer.Core
{
    public class HttpClientUtility : IHttpClientUtility
    {
        private readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string BASE_URL;
        public HttpClientUtility(IConfiguration configuration, ILogger logger, ICustomHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.Create();
            _configuration = configuration;
            _logger = logger;
            BASE_URL = _configuration[ClientConstants.AppSettingsKey.ServerUrl] ?? "";
        }

        public Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            ImageInfoModel imagesInfo = new ImageInfoModel();
            try
            {
                string URL = @$"{BASE_URL}{keyword}/{pageNo}/{itemsPerPage}";
                
                var response = _httpClient.GetStringAsync(URL).GetAwaiter().GetResult();
                
                imagesInfo = JsonSerializer.Deserialize<ImageInfoModel>(response) ?? new ImageInfoModel();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in HttpClientUtility.GetImages, ExMsg : {ex.Message}, ExStackTarce: {ex.StackTrace}");
            }
            return Task.FromResult(imagesInfo);
        }

    }
}
