using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUtility
{
    public class CustomHttpClientFactory : ICustomHttpClientFactory
    {
        private HttpClient _httpClient = null;
        public HttpClient Create()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            return _httpClient;
        }
    }
}
