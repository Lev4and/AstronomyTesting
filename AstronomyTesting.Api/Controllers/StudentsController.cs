using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost("addStudent")]
        public async Task<IActionResult> PostAddStudent([FromBody]dynamic obj)
        {
            try
            {
                if (_dataManager.Students.AddStudent(Convert.ToInt32(obj.UserId), Convert.ToInt32(obj.GroupId)))
                {
                    return Ok(true);
                }
                else
                {
                    return Conflict();
                }
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
