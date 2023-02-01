﻿namespace Perserverance.Client.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public override void Begin()
        {
            Logger.Info($"[ConnectionManager] Started");
            Event("onResourceStart", new Action<string>(OnResourceStartAsync));
        }

        /// <summary>
        /// Called when the resource starts on the client
        /// </summary>
        /// <param name="resourceName"></param>
        private async void OnResourceStartAsync(string resourceName)
        {
            if (resourceName != GetCurrentResourceName()) return;

            Session.IsSessionReady = await EventDispatcher.Get<bool>("connection:active", Game.Player.ServerId);

            NuiManager.SetFocus(true);
            NuiManager.SendMessage(new { action = "setVisible", data = true });

            Logger.Debug("Perserverance.Client has started!");
        }
    }
}