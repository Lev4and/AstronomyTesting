using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class AccountRestClient : BaseRestClient
    {
        public AccountRestClient() : base("https://localhost:5001/api/account/")
        {

        }

        public Task<HttpResponseMessage> GetUsers()
        {
            try
            {
                return _client.GetAsync("users");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsUser(string fullName, string password)
        {
            try
            {
                return _client.GetAsync($"containsUser/fullName={fullName}&password={password}");
            }
            catch
            {
                return null;
            }
        }
    }
}
