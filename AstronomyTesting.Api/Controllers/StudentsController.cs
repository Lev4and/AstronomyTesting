using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/students/")]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private DataManager _dataManager;

        public StudentsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpPut("addStudent")]
        [Route("addStudent/userId={userId}&groupId={groupId}")]
        public async Task<IActionResult> PutAddStudent(int userId, int groupId)
        {
            try
            {
                return Ok(_dataManager.Students.AddStudent(userId, groupId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("containsStudent")]
        [Route("containsStudent/userId={userId}")]
        public async Task<IActionResult> GetContainsStudent(int userId)
        {
            try
            {
                return Ok(_dataManager.Students.ContainsStudent(userId));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("studentByUserId")]
        [Route("studentByUserId/userId={userId}")]
        public async Task<IActionResult> GetStudentByUserId(int userId)
        {
            try
            {
                return Ok(_dataManager.Students.GetStudentByUserId(userId));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
