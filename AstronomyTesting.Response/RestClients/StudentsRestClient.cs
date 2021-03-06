using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class StudentsRestClient : BaseRestClient
    {
        public StudentsRestClient() : base("students")
        {

        }

        public Task<HttpResponseMessage> AddStudent(int userId, int groupId)
        {
            try
            {
                return _client.PostAsync("addStudent", new StringContent($"{JsonConvert.SerializeObject(new { UserId = userId, GroupId = groupId })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsStudent(int userId)
        {
            try
            {
                return _client.GetAsync($"containsStudent/userId={userId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> StudentByUserId(int userId)
        {
            try
            {
                return _client.GetAsync($"studentByUserId/userId={userId}");
            }
            catch
            {
                return null;
            }
        }
    }
}
