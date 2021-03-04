using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class StudentsRestClient : BaseRestClient
    {
        public StudentsRestClient() : base("https://localhost:5001/api/students/")
        {

        }

        public Task<HttpResponseMessage> AddStudent(int userId, int groupId)
        {
            try
            {
                return _client.PutAsync($"addStudent/userId={userId}&groupId={groupId}", new StringContent(""));
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
