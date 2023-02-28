namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal static class AdminController
    {
        const string SNAILY_CAD_ADMIN = "admin";

        internal static async Task<List<PageProperty>> GetServerSideProperties(PerseveranceUser user)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_ADMIN}/values/gender?paths=ethnicity,license,driverslicense_category", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                return null;
            }

            return await resp.GetObjectFromResponseContentAsync<List<PageProperty>>();
        }
    }
}
