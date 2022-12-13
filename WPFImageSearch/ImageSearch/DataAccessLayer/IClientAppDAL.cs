using ImageSearchServer.MVVM.Model;
using System.Collections.Generic;

namespace ImageSearchServer.DataAccesslayer
{
    public interface IClientAppDAL
    {
        ImageInfoModel GetImages(string keyword, int pageNo, int itemsPerPage);
    }
}