using FxEvents;
using FxEvents.Shared;
using FxEvents.Shared.EventSubsystem;
using Perserverance.Server.Models;
using Perserverance.Shared.Models;
using System.Net.Http;

namespace Perserverance.Server.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public override void Begin()
        {
            Event("playerConnecting", new Action<Player, string, CallbackDelegate, dynamic>(OnPlayerConnectingAsync));
            Event("playerJoining", new Action<Player, string>(OnPlayerJoiningAsync));
            Event("playerDropped", new Action<Player, string>(OnPlayerDropped));
            Event("onResourceStop", new Action<string>(OnResourceStop));

            EventDispatcher.Mount("connection:active", new Func<PerserveranceUser, int, Task<bool>>(OnUserActiveAsync));

            EventDispatcher.Mount("server:authenticate", new Func<PerserveranceUser, int, CadAuthenitcation, Task<dynamic>>(OnServerAuthenticateAsync));
        }
        
        // TODO: fix issue with first param
        private async Task<dynamic> OnServerAuthenticateAsync(PerserveranceUser user, int serverId, CadAuthenitcation cadAuthenitcation)
        {
            try
            {
                Logger.Info($"dataBytes {cadAuthenitcation.Username}");
                
                if (user.Handle != serverId) return new { success = false };
                Logger.Info($"Player {user.Handle} is attempting to authenticate with username {cadAuthenitcation.Username}");
                
                //CadAuthenitcation authenitcation = dataBytes.FromBytes<CadAuthenitcation>();

                //Logger.Debug($"[ConnectionManager] Authenticating {authenitcation?.Username}...");

                //Dictionary<int, string> result = new Dictionary<int, string>();

                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");
                Logger.Info("Oi... Hello...");

                // HttpResponseMessage httpResponseMessage = await HttpHandler.OnSnailyCadAsync(HttpMethod.Post, HttpHandler.SNAILY_CAD_AUTH_LOGIN, new { username = data.username, password = data.password });

                // convert cookies into a dictionary
                //Dictionary<string, string> cookies = HttpHandler.GetCookies(httpResponseMessage);

                //Logger.Debug($"Cookies: {cookies.ToJson()}");


                return new { success = false };
            }
            catch
            {
                return new { success = false };
            }
        }

        internal async void OnPlayerConnectingAsync([FromSource] Player player, string name, CallbackDelegate denyWithReason, dynamic deferrals)
        {
            await Main.IsReadyAsync();

            deferrals.defer();
            Logger.Debug($"Player {player.Name} is connecting...");
        }

        internal async void OnPlayerJoiningAsync([FromSource] Player player, string oldId)
        {
            await Main.IsReadyAsync();
            Logger.Debug($"Player {player.Name} ({player.Handle}) is joining the server.");
        }

        internal void OnPlayerDropped([FromSource] Player player, string reason)
        {
            Logger.Debug($"Player '{player.Name}' dropped, reason; {reason}.");
            int playerId = int.Parse(player.Handle);
            if (ActiveSessions.ContainsKey(playerId))
                ActiveSessions.TryRemove(playerId, out PerserveranceUser user);
        }

        internal void OnResourceStop(string resourceName)
        {
            if (resourceName != GetCurrentResourceName()) return;
        }

        internal async Task<bool> OnUserActiveAsync([FromSource] PerserveranceUser user, int serverId)
        {
            if (user.Handle != serverId) return false;
            Logger.Debug($"User with server handle '{user.Handle}' is updating their session state.");
            ActiveSessions.AddOrUpdate(user.Handle, user, (key, value) => user);
            return true;
        }
    }
}
