using FxEvents;
using FxEvents.Shared;
using Perserverance.Client.GameInterface.Events;
using Perserverance.Shared.Models;

namespace Perserverance.Client.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public override void Begin()
        {
            Logger.Info($"[ConnectionManager] Started");
            Event("onResourceStart", new Action<string>(OnResourceStartAsync));

            // NuiManager.RegisterCallback("authenticate", new Func<Dictionary<string, string>, Task<dynamic>>(OnAuthenticationAsync));

            NuiManager.AttachNuiHandler("authenticate", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    CadAuthenitcation authenitcation = new CadAuthenitcation(metadata.Find<string>(0), metadata.Find<string>(1));
                    var result = await EventDispatcher.Get<dynamic>("server:authenticate", Game.Player.ServerId, authenitcation);
                    return new { success = false };
                }
                catch (Exception ex)
                {
                    return new { success = false };
                }
            }));
        }

        private async Task<dynamic> OnAuthenticationAsync(Dictionary<string, string> data)
        {
            try
            {
                CadAuthenitcation authenitcation = new CadAuthenitcation(data["0"], data["1"]);
                var result = await EventDispatcher.Get<dynamic>("server:authenticate", Game.Player.ServerId, authenitcation);
                return new { success = false };
            }
            catch (Exception ex)
            {
                return new { success = false };
            }
        }

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
