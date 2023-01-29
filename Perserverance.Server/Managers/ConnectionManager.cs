﻿using FxEvents;
using Perserverance.Server.Models;

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
