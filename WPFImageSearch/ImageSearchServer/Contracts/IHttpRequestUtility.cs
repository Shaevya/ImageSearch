using ImageSearchServer.MVVM.Model;
namespace ImageSearchServer.Contracts
{
    public interface IHttpRequestUtility
    {
        Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage);
    }
}