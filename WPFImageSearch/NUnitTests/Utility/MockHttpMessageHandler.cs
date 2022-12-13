using System.Net;

namespace NUnitTests.Utility
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private string _response;
        public MockHttpMessageHandler(string response)
        {
            _response = response;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_response)
            };

            return await Task.FromResult(responseMessage);
        }
    }
}
