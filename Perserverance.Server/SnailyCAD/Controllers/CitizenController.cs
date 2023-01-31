using FxEvents.Shared;
using Perserverance.Shared.Models.SnailyCAD;
using System.Net.Http;

namespace Perserverance.Server.SnailyCAD.Controllers
{
    internal static class CitizenController
    {
        const string SNAILY_CAD_CITIZEN = "citizen";

        internal static async Task<Rootobject> GetCitizens(Dictionary<string, string> cookies, string query = "", int skip = 0)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_CITIZEN}?query={query}&skip={skip}", cookies: cookies);

            return resp.Content.ReadAsStringAsync().Result.FromJson<Rootobject>();
        }
    }
}
