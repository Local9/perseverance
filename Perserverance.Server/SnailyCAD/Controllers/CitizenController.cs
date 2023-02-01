namespace Perserverance.Server.SnailyCAD.Controllers
{
    internal static class CitizenController
    {
        const string SNAILY_CAD_CITIZEN = "citizen";

        /// <summary>
        /// Gets all the citizens from the SnailyCAD API for the authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        internal static async Task<CitizenMessage> GetCitizens(PerserveranceUser user, string query = "", int skip = 0)
        {
            Main.Logger.Debug($"Player {user.Handle} is attempting to get citizens with query '{query}' and skip '{skip}'");
            await BaseScript.Delay(0);
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_CITIZEN}?query={query}&skip={skip}", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                Main.Logger.Error($"CitizenController.GetCitizens() was unable to get citizens for user {user.Handle}");
                return null;
            }

            string content = await resp.Content.ReadAsStringAsync();
            CitizenMessage result = JsonConvert.DeserializeObject<CitizenMessage>(content);
            Main.Logger.Debug($"Player {user.Handle} has successfully gotten citizens with query {query} and skip {skip}");
            return result;

            // return await resp.OnGetObjectFromResponseContentAsync<CitizenMessage>();
        }
    }
}
