using backend_se.Common.Consts;
using backend_se.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend_se.Common.Helpers
{
    public static class JWTHelper
    {
        public static string GenerateJWT(UserModel user, string secret, string audience, string issuer)
        {
            var roleName = Enum.GetName((eUserRole)(user.Role));
            if (string.IsNullOrEmpty(roleName))
                return "";

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: cred, audience: audience, issuer: issuer);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}