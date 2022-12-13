using ImageSearchServer.Core;
using ImageSearchServer.MVVM.Model;
using Serilog;
using SharedUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ImageSearchServer.DataAccesslayer
{
    public class ClientAppDAL : IClientAppDAL
    {
        private IHttpClientUtility _httpClient;
        private readonly ILogger _logger;
        public ClientAppDAL(IHttpClientUtility httpClient, ILogger logger)
        {
            _httpClient = httpClient;
        }

        public ImageInfoModel GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            ImageInfoModel images = new ImageInfoModel();

            try
            {
                return _httpClient.GetImages(keyword, pageNo, itemsPerPage).Result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in MainViewModel, ExMsg : {ex.Message}");
            }
            return images;
        }
    }
}
