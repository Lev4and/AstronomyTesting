using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class AccountsRestClient : BaseRestClient
    {
        public AccountsRestClient() : base("account")
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
                return _client.PostAsync("addUser", new StringContent(JsonConvert.SerializeObject(new { RoleId = roleId, FullName = fullName, Password = password }), Encoding.UTF8, "application/json"));
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
