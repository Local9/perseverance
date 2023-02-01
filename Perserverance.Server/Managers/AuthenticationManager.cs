namespace Perserverance.Server.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:authenticate", new Func<PerserveranceUser, int, Authenitcation, Task<EventMessage>>(OnServerAuthenticateAsync));
            EventDispatcher.Mount("server:logout", new Func<PerserveranceUser, int, Task<bool>>(OnServerLogoutAsync));
        }

        private async Task<EventMessage> OnServerAuthenticateAsync([FromSource] PerserveranceUser user, int serverId, Authenitcation auth)
        {
            try
            {
                if (user.Handle != serverId) return null;
                Logger.Debug($"Player {user.Handle} is attempting to authenticate with username {auth.Username}");

                SnailyCadAuthenticationDetails result = await AuthController.Authenticate(auth.Username, auth.Password);
                user.SetSnailyAuth(result);

                Logger.Debug($"Player {user.Handle} has successfully authenticated with username {auth.Username}");

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

        private Task<bool> OnServerLogoutAsync(PerserveranceUser arg1, int arg2)
        {
            throw new NotImplementedException();
        }
    }
}
