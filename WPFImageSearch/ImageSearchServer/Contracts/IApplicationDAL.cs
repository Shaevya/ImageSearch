using ImageSearchServer.MVVM.Model;

namespace ImageSearchServer.Contracts
{
    public interface IApplicationDAL
    {
        Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage);
    }
}