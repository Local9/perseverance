namespace Perserverance.Server.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:authenticate", new Func<EventSource, int, Authenitcation, Task<EventMessage>>(OnServerAuthenticateAsync));
            EventDispatcher.Mount("server:logout", new Func<EventSource, int, Task<bool>>(OnServerLogoutAsync));
        }

        private async Task<EventMessage> OnServerAuthenticateAsync([FromSource] EventSource source, int serverId, Authenitcation auth)
        {
            try
            {
                if (source.Handle != serverId) return null;

                Logger.Debug($"Player {source.Handle} is attempting to authenticate with username {auth.Username}");

                SnailyCadAuthenticationDetails result = await AuthController.Authenticate(auth.Username, auth.Password);
                source.User.SetSnailyAuth(result);

                Logger.Debug($"Player {source.Handle} has successfully authenticated with username {auth.Username}");

                return new EventMessage
                {
                    success = true,
                    message = "Successfully authenticated",
                };
            }
            catch
            {
                return null;
            }
        }

        private Task<bool> OnServerLogoutAsync(EventSource arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }
}
