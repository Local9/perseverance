namespace Perserverance.Server.SnailyCAD
{
    internal static class HttpHandler
    {
        private static readonly HttpClient HttpClient = new(new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
            UseCookies = false
        });


        internal static async Task<HttpResponseMessage> OnHttpResponseMessageAsync(HttpMethod httpMethod, string endpoint = "", object data = null, Dictionary<string, string> cookies = null)
        {
            Main.Logger.Debug($"HttpHandler.OnHttpResponseMessageAsync() is attempting to send a {httpMethod} request to {endpoint}");
            try
            {
                
                var baseAddress = new Uri($"{Main.SnailyCadUrl}/v1");
                HttpClient.BaseAddress = baseAddress;

                using (var request = new HttpRequestMessage(httpMethod, endpoint))
                {
                    if (cookies is not null)
                    {
                        foreach (var cookie in cookies)
                        {
                            request.Headers.Add("Cookie", $"{cookie.Key}={cookie.Value}");
                        }
                    }

                    if (data is not null)
                    {
                        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    }

                    using (var response = await HttpClient.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Main.Logger.Debug($"HttpHandler.OnHttpResponseMessageAsync() successfully sent a {httpMethod} request to {endpoint}");
                            return response;
                        }
                        else
                        {
                            Main.Logger.Error($"HttpHandler.OnHttpResponseMessageAsync() was unable to send a {httpMethod} request to {endpoint}");
                            return null;
                        }
                    }
                }



                //using (var handler = new HttpClientHandler { UseCookies = false })
                //using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                //{
                //    var message = new HttpRequestMessage(httpMethod, endpoint);
                //    message.Headers.Add("accept", "*/*");
                //    // message.Headers.Add("snaily-cad-api-token", Main.SnailyCadApiKey); // this ignores authentication
                //    message.Headers.Add("is-from-dispatch", "false");

                //    if (data is not null)
                //        message.Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json");

                //    if (cookies is not null)
                //    {
                //        string cookieString = "";
                //        foreach (var cookie in cookies)
                //        {
                //            cookieString += $"{cookie.Key}={cookie.Value};";
                //        }
                //        message.Headers.Add("Cookie", cookieString);
                //    }

                //    var result = await client.SendAsync(message).ConfigureAwait(false); ;

                //    Main.Logger.Debug($"HttpHandler.OnHttpResponseMessageAsync() has successfully sent a {httpMethod} request to {endpoint}");

                //    return result;
                //}
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

        internal static async Task<T> OnGetObjectFromResponseContentAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            string json = await httpResponseMessage.Content.ReadAsStringAsync();

            Main.Logger.Info($"HTTP Handler received response: {json}");

            return json.FromJson<T>();
        }
    }
}
