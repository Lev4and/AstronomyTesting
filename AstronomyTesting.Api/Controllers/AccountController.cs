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

        [HttpGet("users")]
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
                throw new ArgumentNullException("fullName", "Полное имя пользователя не может быть пустым или длиной 0 символов.");
            }

            if (password != null ? password.Length == 0 : true)
            {
                throw new ArgumentNullException("password", "Пароль не может быть пустым или длиной 0 символов.");
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
    }
}
