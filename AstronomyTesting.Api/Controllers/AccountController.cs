using AstronomyTesting.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
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

        [HttpPost("addUser")]
        public async Task<IActionResult> PostAddUser([FromBody]dynamic obj)
        {
            try
            {
                if(_dataManager.Users.AddUser(Convert.ToInt32(obj.RoleId), Convert.ToString(obj.FullName), Convert.ToString(obj.Password)))
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
