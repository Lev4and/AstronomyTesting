using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstronomyTesting.Api.Controllers
{
    [ApiController]
    [Route("api/account/")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private DataManager _dataManager;

        public AccountController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(_dataManager.Users.GetUsers().ToList());
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("containsUser")]
        [Route("containsUser/fullName={fullName}&password={password}")]
        public async Task<IActionResult> GetContainsUser(string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                return BadRequest();
            }

            if (password != null ? password.Length == 0 : true)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_dataManager.Users.ContainsUser(fullName, password));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("addUser")]
        [Route("addUser/roleId={roleId}&fullName={fullName}&password={password}")]
        public async Task<IActionResult> PutAddUser(int roleId, string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                return BadRequest();
            }

            if (password != null ? password.Length == 0 : true)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_dataManager.Users.AddUser(roleId, fullName, password));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("userByFullNameAndPassword")]
        [Route("userByFullNameAndPassword/fullName={fullName}&password={password}")]
        public async Task<IActionResult> GetUserByFullNameAndPassword(string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                return BadRequest();
            }

            if (password != null ? password.Length == 0 : true)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_dataManager.Users.GetUserByFullNameAndPassword(fullName, password));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
