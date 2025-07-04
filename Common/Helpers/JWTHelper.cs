using backend_se.Common.Consts;
using backend_se.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend_se.Common.Helpers
{
    public static class JWTHelper
    {
        public static void GenerateJWT(HttpResponse response, UserModel user, string secret, string audience, string issuer)
        {
            var roleName = Enum.GetName((eUserRole)(user.Role));
            if (string.IsNullOrEmpty(roleName))
                return;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(10), signingCredentials: cred, audience: audience, issuer: issuer);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            SecureCookieHelper.AppendSecureCookie(response, "TokenJWT", jwt, DateTime.UtcNow.AddMinutes(10));
        }

        public static long? RefreshTokenUserId(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var refreshToken = StaticData.RefreshTokens.FirstOrDefault(x => !x.Revoked && x.Id.ToString() == token && x.Expires > DateTime.UtcNow);
            if (refreshToken == null)
                return null;

            return refreshToken.UserId;
        }

        public static void DeleteTokenJWT(HttpResponse response)
        {
            SecureCookieHelper.DeleteSecureCookie(response, "TokenJWT");
        }

        public static void GenerateRefreshToken(HttpResponse response, long userId)
        {
            RevokeUserRefreshTokens(userId);

            var newToken = new RefreshTokenModel { Id = Guid.NewGuid(), UserId = userId, Revoked = false, Expires = DateTime.UtcNow.AddDays(7) };
            StaticData.RefreshTokens.Add(newToken);

            SecureCookieHelper.AppendSecureCookie(response, "RefreshToken", newToken.Id.ToString(), DateTime.UtcNow.AddDays(7));
        }

        public static void RevokeUserRefreshTokens(long userId)
        {
            var toBeRevoked = StaticData.RefreshTokens.Where(x => x.UserId == userId);

            foreach(var token in toBeRevoked)
                token.Revoked = true;
        }

        public static void RevokeRefreshToken(string refreshToken)
        {
            var token = StaticData.RefreshTokens.FirstOrDefault(x => x.Id.ToString() == refreshToken);
            if (token == null)
                return;

            token.Revoked = true;
        }
    }
}