using backend_se.Common.Consts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_se.Common.Controllers
{
    public class BaseController : ControllerBase
    {
        public long? UserId
        {
            get
            {
                var id = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;

                if(!long.TryParse(id, out long userId))
                    return null;

                return userId;
            }
        }

        public eUserRole? Role
        {
            get
            {
                var role = User.Claims.First(x => x.Type == ClaimTypes.Role).Value;

                if (!Enum.TryParse(role, out eUserRole userRole))
                    return null;

                return userRole;
            }
        }

        public string? RefreshToken
        {
            get
            {
                return Request.Cookies["RefreshToken"];
            }
        }
    }
}
