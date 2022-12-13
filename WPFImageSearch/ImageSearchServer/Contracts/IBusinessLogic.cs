using ImageSearchServer.MVVM.Model;

namespace ImageSearchServer.Contracts
{
    public interface IBusinessLogic
    {
        Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage);
    }
}