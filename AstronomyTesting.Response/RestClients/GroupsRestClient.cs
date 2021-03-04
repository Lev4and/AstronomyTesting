using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class GroupsRestClient : BaseRestClient
    {
        public GroupsRestClient() : base("https://localhost:5001/api/groups/")
        {

        }

        public Task<HttpResponseMessage> GetGroups()
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
    }
}
