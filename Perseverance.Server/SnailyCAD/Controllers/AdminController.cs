namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal static class AdminController
    {
        const string SNAILY_CAD_ADMIN = "admin";

        /// <summary>
        /// Gets a list of server side properties from the SnailyCAD API for the authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static async Task<List<PagePropsValue>> GetServerSidePropertiesAsync(PerseveranceUser user)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_ADMIN}/values/gender?paths=ethnicity,license,driverslicense_category", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                return null;
            }

            return await resp.GetObjectFromResponseContentAsync<List<PagePropsValue>>();
        }

        /// <summary>
        /// Gets a list of addresses from the SnailyCAD API for the authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        internal static async Task<List<Address>> GetAddressesAsync(PerseveranceUser user, string searchQuery = "")
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_ADMIN}/values/address/search?query={searchQuery}", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                return null;
            }

            return await resp.GetObjectFromResponseContentAsync<List<Address>>();
        }
    }
}
