using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class QuestionsRestClient : BaseRestClient
    {
        public QuestionsRestClient() : base("questions")
        {

        }

        public Task<HttpResponseMessage> GetQuestionsByTestId(int testId)
        {
            try
            {
                return _client.GetAsync($"questionsByTestId/testId={testId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> AddQuestion(int testId, string name, byte[] photo)
        {
            try
            {
                return _client.PostAsync("addQuestion", new StringContent($"{JsonConvert.SerializeObject(new { TestId = testId, Name = name, Photo = photo })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsQuestion(int testId, string name)
        {
            try
            {
                return _client.GetAsync($"containsQuestion/testId={testId}&name={name}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetQuestionById(int id)
        {
            try
            {
                return _client.GetAsync($"questionById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> RemoveQuestionById(int id)
        {
            try
            {
                return _client.DeleteAsync($"removeQuestionById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> UpdateQuestionById(int id, int testId, string name, byte[] photo)
        {
            try
            {
                return _client.PutAsync("updateQuestionById", new StringContent($"{JsonConvert.SerializeObject(new { Id = id, TestId = testId, Name = name, Photo = photo })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }
    }
}
