namespace Perseverance.Server.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:authenticate", new Func<EventSource, int, Authentication, Task<dynamic>>(OnServerAuthenticateAsync));
            EventDispatcher.Mount("server:getRegistrationUrl", new Func<EventSource, int, Task<string>>(OnGetServerRegistrationUrl));
            EventDispatcher.Mount("server:logout", new Func<EventSource, int, Task<bool>>(OnServerLogoutAsync));
        }

        /// <summary>
        /// Returns the registration URL
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <returns></returns>
        private Task<string> OnGetServerRegistrationUrl([FromSource] EventSource source, int serverId)
        {
            if (source.Handle != serverId) return Task.FromResult<string>(null);

            if (Main.SnailyCadUrl == "unknown" || string.IsNullOrEmpty(Main.SnailyCadUrl) || string.IsNullOrWhiteSpace(Main.SnailyCadUrl) || Main.SnailyCadUrl.Contains("api"))
            {
                Logger.Error("SnailyCAD URL is unknown or set incorrectly, please set the convar 'snailycad' to be the URL of the CAD.");
                return Task.FromResult("unknown");
            };

            return Task.FromResult($"{Main.SnailyCadUrl}/auth/register");
        }

        /// <summary>
        /// Called when the client is attempting to authenticate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        private async Task<dynamic> OnServerAuthenticateAsync([FromSource] EventSource source, int serverId, Authentication auth)
        {
            if (source.Handle != serverId) return null;

            Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' is attempting to authenticate with username '{auth.Username}'");

            object result = await AuthController.AuthenticateAsync(auth.Username, auth.Password);

            Type type = result.GetType();

            if (type == typeof(ErrorMessage))
            {
                return result;
            }
            else if (type == typeof(SnailyCadAuthenticationDetails))
            {
                SnailyCadAuthenticationDetails snailyCadAuthenticationDetails = result as SnailyCadAuthenticationDetails;
                source.User.SetSnailyAuth(snailyCadAuthenticationDetails);

                Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' has successfully authenticated with username '{auth.Username}'");
            }

            // we just have to return something
            return new { success = true };
        }

        /// <summary>
        /// Called when the client is attempting to logout
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Task<bool> OnServerLogoutAsync([FromSource] EventSource source, int serverId)
        {
            throw new NotImplementedException();
        }
    }
}
