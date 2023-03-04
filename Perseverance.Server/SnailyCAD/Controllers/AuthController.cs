namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal class AuthController
    {
        const string SNAILY_CAD_AUTH_LOGIN = "auth/login";

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
    }
}
