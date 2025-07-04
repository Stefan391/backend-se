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
            var user = _userProvider.Login(req);

            if (user == null)
                return BadRequest();

            if (!CreateAccessToken(user.Id))
                return BadRequest();

            JWTHelper.GenerateRefreshToken(Response, user.Id);

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
            JWTHelper.DeleteTokenJWT(Response);
            JWTHelper.RevokeUserRefreshTokens(UserId ?? 0);

            return Ok(new { ok = true });
        }

        [HttpGet("refresh-token")]
        [AllowAnonymous]
        public IActionResult RefreshJWTToken()
        {
            JWTHelper.DeleteTokenJWT(Response);

            var userId = JWTHelper.RefreshTokenUserId(RefreshToken ?? "");
            if (userId == null)
                return BadRequest();

            return CreateAccessToken(userId ?? 0) ? Ok() : BadRequest();
        }

        private bool CreateAccessToken(long userId)
        {
            var secret = _appSettings["JWTInfo:secret"];
            var issuer = _appSettings["JWTInfo:ValidIssuer"];
            var audience = _appSettings["JWTInfo:ValidAudience"];

            var user = _userProvider.GetById(userId);
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                return false;

            JWTHelper.GenerateJWT(Response, user, secret, issuer, audience);
            return true;
        }
    }
}
