using Perseverance.Shared.Models.Generic;

namespace Perseverance.Server.SnailyCAD.Controllers
{
    internal static class AdminController
    {
        const string SNAILY_CAD_ADMIN = "admin";

        internal static async Task<List<TypeList>> GetServerSideProperties(PerseveranceUser user)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_ADMIN}/values/gender?paths=ethnicity,license,driverslicense_category", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                return null;
            }

            List<PageProperty> pageProperties = await resp.GetObjectFromResponseContentAsync<List<PageProperty>>();
            List<TypeList> lst = new();

            foreach (PageProperty pageProperty in pageProperties)
            {
                PageProperties[] values = pageProperty.values;
                TypeList typeList = new()
                {
                    type = pageProperty.type,
                    values = values.Select(x => new ListItem() { value = x.id, label = x.value }).ToList()
                };
                lst.Add(typeList);
            }

            return lst;
        }

        internal static async Task<List<TypeList>> GetAddresses(PerseveranceUser user, string searchQuery)
        {
            HttpResponseMessage resp = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, $"{SNAILY_CAD_ADMIN}/values/address/search?query={searchQuery}", cookies: user.SnailyAuth.Cookies);

            if (resp is null)
            {
                return null;
            }

            List<PageProperty> pageProperties = await resp.GetObjectFromResponseContentAsync<List<PageProperty>>();
            List<TypeList> lst = new();

            foreach (PageProperty pageProperty in pageProperties)
            {
                PageProperties[] values = pageProperty.values;
                TypeList typeList = new()
                {
                    type = pageProperty.type,
                    values = values.Select(x => new ListItem() { value = x.id, label = x.value }).ToList()
                };
                lst.Add(typeList);
            }

            return lst;
        }
    }
}
