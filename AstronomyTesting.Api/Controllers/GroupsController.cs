using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/groups/")]
    [Produces("application/json")]
    public class GroupsController : ControllerBase
    {
        private DataManager _dataManager;

        public GroupsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                return Ok(_dataManager.Groups.GetGroups().ToList());
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
