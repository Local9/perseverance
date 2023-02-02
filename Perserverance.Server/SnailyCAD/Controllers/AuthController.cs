namespace Perserverance.Server.SnailyCAD.Controllers
{
    internal class AuthController
    {
        const string SNAILY_CAD_AUTH_LOGIN = "auth/login";
        const string SNAILY_CAD_AUTH_REGISTER = "auth/register";

        internal static async Task<SnailyCadAuthenticationDetails> Authenticate(string username, string password)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Post, SNAILY_CAD_AUTH_LOGIN, new { username, password });

            await BaseScript.Delay(0);

            if (resp != null)
            {
                SnailyCadAuthenticationDetails snailyAuthentication = new();
                snailyAuthentication.Cookies = HttpHandler.GetCookies(resp);

                Dictionary<string, string> userId = await resp.OnGetObjectFromResponseContentAsync<Dictionary<string, string>>();
                snailyAuthentication.UserId = userId["userId"];

                return snailyAuthentication;
            }

            return null;
        }
    }
}
