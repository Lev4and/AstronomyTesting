using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class RolesRestClient : BaseRestClient
    {
        public RolesRestClient() : base("https://localhost:5001/api/roles/")
        {

        }

        public Task<HttpResponseMessage> GetRoles()
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

        public Task<HttpResponseMessage> GetRoleIdByRoleName(string roleName)
        {
            try
            {
                return _client.GetAsync($"roleIdByRoleName/roleName={roleName}");
            }
            catch
            {
                return null;
            }
        }
    }
}
