using ImageSearchServer.MVVM.Model;
using ImageSearchServer.Contracts;

namespace ImageSearchServer.DataAccesslayer
{
    public class ApplicationDAL : IApplicationDAL
    {
        private readonly IHttpRequestUtility _httpUtility;

        public ApplicationDAL(IHttpRequestUtility httpUtility)
        {
            _httpUtility = httpUtility;
        }

        public Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            return _httpUtility.GetImages(keyword, pageNo, itemsPerPage);
        }
    }
}
