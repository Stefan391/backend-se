namespace backend_se.Common.Helpers
{
    public static class SecureCookieHelper
    {
        public static void AppendSecureCookie(HttpResponse response, string name, string value, DateTime expireDate)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
                return;

            response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expireDate
            });
        }

        public static void DeleteSecureCookie(HttpResponse response, string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            response.Cookies.Delete(name);
        }
    }
}
