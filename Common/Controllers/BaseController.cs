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
                var id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                if (id == null)
                    return null;

                if(!long.TryParse(id.Value, out long userId))
                    return null;

                return userId;
            }
        }

        public eUserRole? Role
        {
            get
            {
                var role = User.Claims.First(x => x.Type == ClaimTypes.Role);
                if (role == null)
                    return null;

                if (!Enum.TryParse(role.Value, out eUserRole userRole))
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
