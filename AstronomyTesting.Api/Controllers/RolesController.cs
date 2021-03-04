using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/roles/")]
    [Produces("application/json")]
    public class RolesController : ControllerBase
    {
        private DataManager _dataManager;

        public RolesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                return Ok(_dataManager.Roles.GetRoles().ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("roleIdByRoleName")]
        [Route("roleIdByRoleName/roleName={roleName}")]
        public async Task<IActionResult> GetRoleIdByRoleName(string roleName)
        {
            if (roleName != null ? roleName.Length == 0 : true)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_dataManager.Roles.GetRoleIdByRoleName(roleName));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
