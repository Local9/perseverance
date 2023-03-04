namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal class AuthController
    {
        const string SNAILY_CAD_AUTH_LOGIN = "auth/login";
        const string SNAILY_CAD_AUTH_REGISTER = "auth/register";

        /// <summary>
        /// Authenticates a user with the SnailyCAD API
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Success: SnailyCadAuthenticationDetails, Error: ErrorMessage</returns>
        internal static async Task<object> Authenticate(string username, string password)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Post, SNAILY_CAD_AUTH_LOGIN, new { username, password });

            if (resp.IsSuccessStatusCode)
            {
                SnailyCadAuthenticationDetails snailyAuthentication = new();
                snailyAuthentication.Cookies = HttpHandler.GetCookies(resp);

                Dictionary<string, string> requestData = await resp.GetObjectFromResponseContentAsync<Dictionary<string, string>>();
                snailyAuthentication.UserId = requestData["userId"];

                return snailyAuthentication;
            }

            return await resp.GetObjectFromResponseContentAsync<ErrorMessage>();
        }

        /// <summary>
        /// Registers a new user with the SnailyCAD API
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        internal async static Task<SnailyCadAuthenticationDetails> Register(Registration registration)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Post, SNAILY_CAD_AUTH_REGISTER, registration);

            await BaseScript.Delay(0);

            if (resp != null)
            {
                SnailyCadAuthenticationDetails snailyAuthentication = new();
                snailyAuthentication.Cookies = HttpHandler.GetCookies(resp);

                Dictionary<string, string> requestData = await resp.GetObjectFromResponseContentAsync<Dictionary<string, string>>();
                snailyAuthentication.UserId = requestData["userId"];

                if (bool.TryParse(requestData["isOwner"], out bool isOwner))
                    snailyAuthentication.IsOwner = isOwner;

                if (Enum.TryParse(requestData["whitelistStatus"], out WhitelistStatus whitelistStatus))
                    snailyAuthentication.WhitelistStatus = whitelistStatus;

                return snailyAuthentication;
            }

            return null;
        }
    }
}
