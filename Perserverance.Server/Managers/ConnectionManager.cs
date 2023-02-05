using CitizenFX.Core;
using Perserverance.Shared;

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

            EventDispatcher.Mount("connection:active", new Func<EventSource, int, Task<EventMessage>>(OnUserActiveAsync));
        }

        /// <summary>
        /// Called when a player is connecting to the server
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="denyWithReason"></param>
        /// <param name="deferrals"></param>
        internal async void OnPlayerConnectingAsync([FromSource] Player player, string name, CallbackDelegate denyWithReason, dynamic deferrals)
        {
            await Main.IsReadyAsync();

            deferrals.defer();
            Logger.Debug($"Player {player.Name} is connecting...");
        }

        /// <summary>
        /// Called when a player is joining the server
        /// </summary>
        /// <param name="player"></param>
        /// <param name="oldId"></param>
        internal async void OnPlayerJoiningAsync([FromSource] Player player, string oldId)
        {
            await Main.IsReadyAsync();
            Logger.Debug($"Player {player.Name} ({player.Handle}) is joining the server.");
            SetPlayerRoutingBucket(player.Handle, 900000 + int.Parse(player.Handle));
        }

        /// <summary>
        /// Called when a player is dropped from the server
        /// </summary>
        /// <param name="player"></param>
        /// <param name="reason"></param>
        internal void OnPlayerDropped([FromSource] Player player, string reason)
        {
            Logger.Debug($"Player '{player.Name}' dropped, reason; {reason}.");
            int playerId = int.Parse(player.Handle);
            if (ActiveSessions.ContainsKey(playerId))
                ActiveSessions.TryRemove(playerId, out PerserveranceUser user);
        }

        /// <summary>
        /// Called when a resource is stopped
        /// </summary>
        /// <param name="resourceName"></param>
        internal void OnResourceStop(string resourceName)
        {
            if (resourceName != GetCurrentResourceName()) return;
        }

        /// <summary>
        /// Called when a user is active
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <returns></returns>
        internal async Task<EventMessage> OnUserActiveAsync([FromSource] EventSource source, int serverId)
        {
            EventMessage eventMessage = new();
            try
            {
                if (source.Handle != serverId)
                {
                    eventMessage.errorMessage = "Invalid server handle.";
                    return eventMessage;
                }

                bool showLandingPage = GetResourceMetadata(GetCurrentResourceName(), "use_landing_page", 0) == "true";

                if (showLandingPage)
                    SetPlayerRoutingBucket($"{source.Handle}", 900000 + source.Handle);

                Logger.Debug($"User with server handle '{source.Handle}' is updating their session state.");
                PerserveranceUser user = new PerserveranceUser(source.Handle);
                ActiveSessions.AddOrUpdate(source.Handle, user, (key, value) => user);

                user.Player.State.Set(StateBagKey.PlayerName, user.Player.Name, true);
                user.Player.State.Set(StateBagKey.CharacterName, $"Unknown Character {user.Handle}", true);

                return eventMessage;
            }
            catch (Exception ex)
            {
                Logger.Error($"[ConnectionManager] OnUserActiveAsync() = >{ex.Message}");
                Logger.Error($"{ex}");
                eventMessage.errorMessage = "[ConnectionManager] Error when trying to activate user.";
                return eventMessage;
            }
        }
    }
}
