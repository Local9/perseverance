using Perseverance.Server.SnailyCAD.Domain;

namespace Perseverance.Server.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:authenticate", new Func<EventSource, int, Authentication, Task<EventMessage>>(OnServerAuthenticateAsync));
            EventDispatcher.Mount("server:register", new Func<EventSource, int, Registration, Task<RegistrationMessage>>(OnServerRegisterAsync));
            EventDispatcher.Mount("server:logout", new Func<EventSource, int, Task<bool>>(OnServerLogoutAsync));
        }

        /// <summary>
        /// Called when the client is attempting to authenticate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        private async Task<EventMessage> OnServerAuthenticateAsync([FromSource] EventSource source, int serverId, Authentication auth)
        {
            try
            {
                if (source.Handle != serverId) return null;

                Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' is attempting to authenticate with username '{auth.Username}'");

                SnailyCadAuthenticationDetails result = await AuthController.Authenticate(auth.Username, auth.Password);

                if (result == null) return new EventMessage()
                {
                    errorMessage = "Invalid username or password"
                };

                source.User.SetSnailyAuth(result);

                Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' has successfully authenticated with username '{auth.Username}'");

                return new EventMessage();
            }
            catch (HttpRequestException ex)
            {
                return new EventMessage()
                {
                    errorMessage = ex.Message
                };
            }
            catch
            {
                return new EventMessage()
                {
                    errorMessage = "Error communicating with the server"
                };
            }
        }

        /// <summary>
        /// Called when the client is attempting to register
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="registration"></param>
        /// <returns></returns>
        private async Task<RegistrationMessage> OnServerRegisterAsync([FromSource] EventSource source, int serverId, Registration registration)
        {
            try
            {
                if (source.Handle != serverId) return null;

                Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' is attempting to register with username '{registration.username}'");

                SnailyCadAuthenticationDetails result = await AuthController.Register(registration);
                source.User.SetSnailyAuth(result);

                Logger.Debug($"Player '{source.Player.Name}#{source.Handle}' has successfully authenticated with username '{registration.username}'");

                return new RegistrationMessage()
                {
                    whitelistStatus = $"{result.WhitelistStatus}",
                    isOwner = result.IsOwner
                };
            }
            catch (HttpRequestException ex)
            {
                return new RegistrationMessage()
                {
                    errorMessage = ex.Message
                };
            }
            catch
            {
                return new RegistrationMessage()
                {
                    errorMessage = "Error communicating with the server"
                };
            }
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
