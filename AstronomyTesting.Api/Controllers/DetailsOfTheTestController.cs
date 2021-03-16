using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/detailsOfTheTest/")]
    [Produces("application/json")]
    public class DetailsOfTheTestController : ControllerBase
    {
        private DataManager _dataManager;

        public DetailsOfTheTestController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet("detailsOfTheStudentPassingTheTest")]
        [Route("detailsOfTheStudentPassingTheTest/testId={testId}&studentId={studentId}")]
        public async Task<IActionResult> GetDetailsOfTheStudentPassingTheTest(int testId, int studentId)
        {
            try
            {
                return Ok(_dataManager.DetailsOfTheTest.GetDetailsOfTheStudentPassingTheTest(testId, studentId).ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("detailsOfTheTest")]
        [Route("detailsOfTheTest/testId={testId}")]
        public async Task<IActionResult> GetDetailsOfTheTest(int testId)
        {
            try
            {
                return Ok(_dataManager.DetailsOfTheTest.GetDetailsOfTheTest(testId).ToList());
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
