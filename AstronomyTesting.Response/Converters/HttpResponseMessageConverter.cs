using AstronomyTesting.Response.Service;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.Converters
{
    public static class HttpResponseMessageConverter
    {
        public static T GetResult<T>(Task<HttpResponseMessage> httpResponseMessage)
        {
            if (httpResponseMessage.Result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(httpResponseMessage.Result.Content.ReadAsStringAsync().Result);
            }

            HttpStatusCodeMessageService.GetErrorMessages(httpResponseMessage.Result.StatusCode);

            return default(T);
        }
    }
}
