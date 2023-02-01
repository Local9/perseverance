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
        internal static async Task<EventMessage> GetCitizens(PerserveranceUser user, string query = "", int skip = 0)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_CITIZEN}?query={query}&skip={skip}", cookies: user.SnailyAuth.Cookies);
            return resp.GetObjectFromResponseContent<EventMessage>();
        }
    }
}
