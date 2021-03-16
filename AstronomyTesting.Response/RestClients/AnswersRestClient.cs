using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class AnswersRestClient : BaseRestClient
    {
        public AnswersRestClient() : base ("answers")
        {

        }

        public Task<HttpResponseMessage> GetAnswersByQuestionId(int questionId)
        {
            try
            {
                return _client.GetAsync($"answersByQuestionId/questionId={questionId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> AddAnswer(int questionId, string name, bool isCorrect)
        {
            try
            {
                return _client.PostAsync("addAnswer", new StringContent($"{JsonConvert.SerializeObject(new { QuestionId = questionId, Name = name, IsCorrect = isCorrect })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsAnswer(int questionId, string name)
        {
            try
            {
                return _client.GetAsync($"containsAnswer/questionId={questionId}&name={name}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsCorrectedAnswersByQuestionId(int questionId)
        {
            try
            {
                return _client.GetAsync($"getContainsCorrectedAnswersByQuestionId/questionId={questionId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetAnswerById(int id)
        {
            try
            {
                return _client.GetAsync($"answerById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> RemoveAnswerById(int id)
        {
            try
            {
                return _client.DeleteAsync($"removeAnswerById/id={id}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> UpdateAnswerById(int id, int questionId, string name, bool isCorrect)
        {
            try
            {
                return _client.PutAsync("updateAnswerById", new StringContent($"{JsonConvert.SerializeObject(new { Id = id, QuestionId = questionId, Name = name, IsCorrect = isCorrect })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }
    }
}
