using ImageSearchServer.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageSearchServer.Core
{
    public interface IHttpClientUtility
    {
        Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage);
    }
}