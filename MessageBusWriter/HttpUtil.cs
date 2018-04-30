using System.Net;
using System.Net.Http;

namespace MessagingService
{
    class HttpUtil
    {

        public static HttpStatusCode Send(string jsonOrder)
        {
            // Create a New HttpClient object.
            using (var client = new HttpClient())
            {
                var result = client.PostAsync(AppConstant.VendorUrl, new StringContent(jsonOrder));
                return result.Result.StatusCode;
            }
        }
    }
}
