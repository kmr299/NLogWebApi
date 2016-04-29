using System.Net.Http;
using Newtonsoft.Json;

namespace NLogWebApiConfig
{
    public static class HttpRequestMessageUtil
    {
        public static string RequestToString(this HttpRequestMessage request)
        {
            var content = "no content";

            try
            {
                var taskRequest = request.Content.ReadAsStringAsync();

                taskRequest.Wait(5000);

                content = taskRequest.Result;
            }
            catch
            {

            }

            var jsonConfig = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            var headerJson = JsonConvert.SerializeObject(request.Headers, jsonConfig);

            return $"Method: {request.Method} | Url: {request.RequestUri} | Body: {content} | Headers: {headerJson}";
        }
    }
}