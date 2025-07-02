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
    [Authorize]
    [ApiController]
    public class AuthController : BaseController
    {
        private UserProvider _userProvider;
        private IConfiguration _appSettings;

        public AuthController(IDataProvider<UserModel> dataProvider, IConfiguration appSettings)
        {
            _userProvider = (UserProvider)dataProvider;
            _appSettings = appSettings;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult LogIn(LoginDTO req)
        {
            var secret = _appSettings["JWTInfo:secret"];
            var issuer = _appSettings["JWTInfo:ValidIssuer"];
            var audience = _appSettings["JWTInfo:ValidAudience"];

            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                return BadRequest();

            var user = _userProvider.Login(req);

            if (user == null)
                return BadRequest();

            var token = JWTHelper.GenerateJWT(user, secret, issuer, audience);

            Response.Cookies.Append("TokenJWT", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok(new { ok = true });
        }

        [HttpGet]
        public IActionResult UserRole()
        {
            return Ok(new { userRole = Role.ToString() });
        }

        [HttpGet("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("TokenJWT");

            return Ok(new { ok = true });
        }
    }
}
