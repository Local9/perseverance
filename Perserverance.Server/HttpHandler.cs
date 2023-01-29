using FxEvents.Shared;
using System.Net.Http;
using System.Text;

namespace Perserverance.Server
{
    internal class HttpHandler
    {
        internal static async Task<HttpResponseMessage> HttpResponseMessageAsync(HttpMethod httpMethod, string url, string endpoint = "", object data = null, Dictionary<string, string> cookies = null)
        {
            try
            {
                var baseAddress = new Uri(url);
                using (var handler = new HttpClientHandler { UseCookies = false })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(httpMethod, $"/{endpoint}");
                    
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
            catch (Exception ex)
            {
                Main.Logger.Error($"HTTP Handler was unable to post to {url}.");
                Main.Logger.Info($"{ex}");
                Main.Logger.Error($"---------------------------------------------.");
                return null;
            }
        }
    }
}
