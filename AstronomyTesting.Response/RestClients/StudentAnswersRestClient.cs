using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class StudentAnswersRestClient : BaseRestClient
    {
        public StudentAnswersRestClient() : base("studentAnswers")
        {

        }

        public Task<HttpResponseMessage> AddStudentAnswer(int studentId, int answerId)
        {
            try
            {
                return _client.PostAsync("addStudentAnswer", new StringContent($"{JsonConvert.SerializeObject(new { StudentId = studentId, AnswerId = answerId })}", Encoding.UTF8, "application/json"));
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> ContainsStudentAnswer(int studentId, int answerId)
        {
            try
            {
                return _client.GetAsync($"containsStudentAnswer/studentId={studentId}&answerId={answerId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetStudentAnswerByAnswerId(int studentId, int answerId)
        {
            try
            {
                return _client.GetAsync($"studentAnswerByAnswerId/studentId={studentId}&answerId={answerId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetStudentAnswerByQuestionId(int studentId, int questionId)
        {
            try
            {
                return _client.GetAsync($"studentAnswerByQuestionId/studentId={studentId}&questionId={questionId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetStudentAnswers(int studentId, int testId)
        {
            try
            {
                return _client.GetAsync($"studentAnswers/studentId={studentId}&testId={testId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> RemoveStudentAnswer(int studentId, int answerId)
        {
            try
            {
                return _client.DeleteAsync($"removeStudentAnswer/studentId={studentId}&answerId={answerId}");
            }
            catch
            {
                return null;
            }
        }
    }
}
