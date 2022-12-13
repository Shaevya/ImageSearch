using ImageSearchServer.MVVM.Model;
using ImageSearchServer.Contracts;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace ImageSearchServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IBusinessLogic _BusinessLogic;
        private readonly ILogger _logger;

        public ImageController(IBusinessLogic businessLogic, ILogger logger)
        {
            _BusinessLogic = businessLogic;
            _logger = logger;
            
        }

        [HttpGet("GetImages/{keyword}/{pageNo}/{itemsPerPage}")]
        public Task<ImageInfoModel> GetImages(string keyword, int pageNo, int itemsPerPage)
        {
            _logger.Information($"Received request for GetImages -> keyword:{keyword}, pageNo:{pageNo}, itemsPerPage:{itemsPerPage}");
            return _BusinessLogic.GetImages(keyword, pageNo, itemsPerPage);
        }
    }
}
