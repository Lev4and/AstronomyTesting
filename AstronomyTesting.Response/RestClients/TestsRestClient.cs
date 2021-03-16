using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class TestsRestClient : BaseRestClient
    {
        public TestsRestClient() : base("tests")
        {

        }

        public Task<HttpResponseMessage> GetTests()
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

        public Task<HttpResponseMessage> AddTest(string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions)
        {
            try
            {
                return _client.PostAsync("addTest", new StringContent($"{JsonConvert.SerializeObject(new { Name = name, Description = description, StartDate = startDate, ExpirationDate = expirationDate, Duration = duration, MaximumNumberOfQuestions = maximumNumberOfQuestions })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsTest(string name)
        {
            try
            {
                return _client.GetAsync($"containsTest/name={name}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetTestById(int id)
        {
            try
            {
                return _client.GetAsync($"testById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> RemoveTestById(int id)
        {
            try
            {
                return _client.DeleteAsync($"removeTestById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> UpdateTestById(int id, string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions)
        {
            try
            {
                return _client.PutAsync("updateTestById", new StringContent($"{JsonConvert.SerializeObject(new { Id = id, Name = name, Description = description, StartDate = startDate, ExpirationDate = expirationDate, Duration = duration, MaximumNumberOfQuestions = maximumNumberOfQuestions })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }
    }
}
