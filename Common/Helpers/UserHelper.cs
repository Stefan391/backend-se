using backend_se.Common.Consts;

namespace backend_se.Common.Helpers
{
    public static class UserHelper
    {
        public static string GetUserRole(short role)
        {
            return ((eUserRole)role).ToString();
        }
    }
}
