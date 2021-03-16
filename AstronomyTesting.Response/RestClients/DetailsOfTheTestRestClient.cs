using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyTesting.Response.RestClients
{
    public class DetailsOfTheTestRestClient : BaseRestClient
    {
        public DetailsOfTheTestRestClient() : base("detailsOfTheTest")
        {

        }

        public Task<HttpResponseMessage> GetDetailsOfTheStudentPassingTheTest(int testId, int studentId)
        {
            try
            {
                return _client.GetAsync($"detailsOfTheStudentPassingTheTest/testId={testId}&studentId={studentId}");
            }
            catch
            {
                return null;
            }
        }

        public Task<HttpResponseMessage> GetDetailsOfTheTest(int testId)
        {
            try
            {
                return _client.GetAsync($"detailsOfTheTest/testId={testId}");
            }
            catch
            {
                return null;
            }
        }
    }
}
