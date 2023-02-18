namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal static class CitizenController
    {
        const string SNAILY_CAD_CITIZEN = "citizen";

        /// <summary>
        /// Creates a citizen in the SnailyCAD API for the authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="citizen"></param>
        /// <returns></returns>
        internal static async Task<CitizenMessage> Create(PerseveranceUser user, Citizen citizen)
        {
            Main.Logger.Debug($"Player {user.Handle} is attempting to create a citizen with name '{citizen.fullname}'");

            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Post, SNAILY_CAD_CITIZEN, citizen, user.SnailyAuth.Cookies);

            if (resp is null)
            {
                Main.Logger.Error($"CitizenController.Create() was unable to create citizen for user {user.Handle}");
                return null;
            }

            return await resp.GetObjectFromResponseContentAsync<CitizenMessage>();
        }

        /// <summary>
        /// Gets all the citizens from the SnailyCAD API for the authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        internal static async Task<CitizenMessage> GetCitizens(PerseveranceUser user, string query = "", int skip = 0)
        {
            Main.Logger.Debug($"Player {user.Handle} is attempting to get citizens with query '{query}' and skip '{skip}'");

            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_CITIZEN}?query={query}&skip={skip}", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                Main.Logger.Error($"CitizenController.GetCitizens() was unable to get citizens for user {user.Handle}");
                return null;
            }

            return await resp.GetObjectFromResponseContentAsync<CitizenMessage>();
        }
    }
}
