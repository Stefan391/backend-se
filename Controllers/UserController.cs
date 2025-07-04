using backend_se.Common.Consts;
using backend_se.Common.Controllers;
using backend_se.Common.Helpers;
using backend_se.Common.Providers;
using backend_se.Data.DTO;
using backend_se.Data.Models;
using backend_se.Data.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_se.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(eUserRole.Admin))]
    public class UserController : BaseController
    {
        private UserProvider _userProvider;
        public UserController(IDataProvider<UserModel> userProvider)
        {
            _userProvider = (UserProvider)userProvider;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(_userProvider.GetAll());
        }

        [HttpGet("user")]
        public IActionResult GetUser(int id)
        {
            var user = _userProvider.GetById(id);

            if (user == null)
                return BadRequest("User doesn't exist");

            return Ok(user);
        }

        [HttpPost("add-user")]
        public IActionResult AddUser(UserModel user)
        {
            var addedUser = _userProvider.Add(user);

            if (addedUser == null)
                return BadRequest("");

            return Ok(addedUser);
        }

        [HttpPost("update-user")]
        public IActionResult UpdateUser(UserModel user)
        {
            var updatedUser = _userProvider.Update(user);

            if (updatedUser == null)
                return BadRequest("User doesn't exist");

            JWTHelper.RevokeUserRefreshTokens(updatedUser.Id);

            return Ok(updatedUser);
        }

        [HttpDelete("delete-user")]
        public IActionResult DeleteUser(long id)
        {
            var deleted = _userProvider.Delete(id);

            if (!deleted)
                return BadRequest();

            return Ok();
        }
    }
}
