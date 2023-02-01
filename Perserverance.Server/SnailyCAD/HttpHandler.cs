namespace Perserverance.Server.SnailyCAD
{
    internal static class HttpHandler
    {
        internal static async Task<HttpResponseMessage> OnHttpResponseMessageAsync(HttpMethod httpMethod, string endpoint = "", object data = null, Dictionary<string, string> cookies = null)
        {
            try
            {
                var baseAddress = new Uri($"{Main.SnailyCadUrl}/v1");
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(httpMethod, endpoint);
                    message.Headers.Add("accept", "*/*");
                    // message.Headers.Add("snaily-cad-api-token", Main.SnailyCadApiKey); // this ignores authentication
                    message.Headers.Add("is-from-dispatch", "false");

                    // message.Headers.Add("Content-Type", "application/json");

                    if (data is not null)
                        message.Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json");

                    if (cookies is not null)
                    {
                        string cookieString = "";
                        foreach (var cookie in cookies)
                        {
                            cookieString += $"{cookie.Key}={cookie.Value};";
                        }
                        message.Headers.Add("Cookie", cookieString);
                    }
                    var result = await client.SendAsync(message);
                    return result.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex) // TODO: Improve error handling
            {
                Main.Logger.Error($"HTTP Handler was unable to {httpMethod} to {Main.SnailyCadUrl}/{endpoint}");
                Main.Logger.Info($"{ex}");
                Main.Logger.Error($"---------------------------------------------");
                return null;
            }
        }
        
        internal static Dictionary<string, string> GetCookies(HttpResponseMessage response)
        {
            var cookieHeader = response.Headers.FirstOrDefault(h => h.Key == "Set-Cookie");
            if (cookieHeader.Value == null)
            {
                return new Dictionary<string, string>();
            }

            var cookies = cookieHeader.Value
                .Select(cookie => cookie.Split(';')[0].Split('='))
                .ToDictionary(keyValue => keyValue[0], keyValue => keyValue[1]);

            return cookies;
        }

        internal static T GetObjectFromResponseContent<T>(this HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadAsStringAsync().Result.FromJson<T>();
        }
    }
}
