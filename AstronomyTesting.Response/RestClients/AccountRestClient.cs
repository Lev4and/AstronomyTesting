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
                return _client.GetAsync("");
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

        public Task<HttpResponseMessage> AddUser(int roleId, string fullName, string password)
        {
            try
            {
                return _client.PutAsync($"addUser/roleId={roleId}&fullName={fullName}&password={password}", new StringContent(""));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetUserByFullNameAndPassword(string fullName, string password)
        {
            try
            {
                return _client.GetAsync($"userByFullNameAndPassword/fullName={fullName}&password={password}");
            }
            catch
            {
                return null;
            }
        }
    }
}
