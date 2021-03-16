using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/tests/")]
    [Produces("application/json")]
    public class TestsController : ControllerBase
    {
        private DataManager _dataManager;

        public TestsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTests()
        {
            try
            {
                return Ok(_dataManager.Tests.GetTests().ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("addTest")]
        public async Task<IActionResult> PostAddTest([FromBody]dynamic test)
        {
            try
            {
                if(_dataManager.Tests.AddTest(Convert.ToString(test.Name), Convert.ToString(test.Description), Convert.ToDateTime(test.StartDate), Convert.ToDateTime(test.ExpirationDate), (int?)test.Duration, Convert.ToInt32(test.MaximumNumberOfQuestions)))
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

        [HttpGet("containsTest")]
        [Route("containsTest/name={name}")]
        public async Task<IActionResult> GetContainsTest(string name)
        {
            try
            {
                if (_dataManager.Tests.ContainsTest(name))
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

        [HttpGet("testById")]
        [Route("testById/id={id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            try
            {
                return Ok(_dataManager.Tests.GetTestById(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("removeTestById")]
        [Route("removeTestById/id={id}")]
        public async Task<IActionResult> DeleteTestById(int id)
        {
            try
            {
                _dataManager.Tests.RemoveTestById(id);

                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("updateTestById")]
        public async Task<IActionResult> PutTestById([FromBody] dynamic test)
        {
            try
            {
                if (_dataManager.Tests.UpdateTestById(Convert.ToInt32(test.Id), Convert.ToString(test.Name), Convert.ToString(test.Description), Convert.ToDateTime(test.StartDate), Convert.ToDateTime(test.ExpirationDate), (int?)test.Duration, Convert.ToInt32(test.MaximumNumberOfQuestions)))
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
    }
}
