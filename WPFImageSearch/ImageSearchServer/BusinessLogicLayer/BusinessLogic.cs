using ImageSearchServer.MVVM.Model;
using ImageSearchServer.Contracts;
using System.Web;

namespace ImageSearchServer.BusinessLogicLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IApplicationDAL _ApplicationDAL;

        public BusinessLogic(IApplicationDAL applicationDAL)
        {
            _ApplicationDAL = applicationDAL;
        }

        public Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            return _ApplicationDAL.GetImages(keyword, pageNo, itemsPerPage);
        }
    }
}
