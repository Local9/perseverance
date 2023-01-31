using FxEvents;
using Perserverance.Client.GameInterface.Events;
using Perserverance.Shared.Models;
using Perserverance.Shared.Models.SnailyCAD;

namespace Perserverance.Client.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public override void Begin()
        {
            Logger.Info($"[ConnectionManager] Started");
            Event("onResourceStart", new Action<string>(OnResourceStartAsync));

            //------------- Commented out as the method currently crashes the client ---------------------
            //NuiManager.RegisterCallback("authenticate", new Func<Dictionary<string, string>, Task<Dictionary<int, string>>>(OnAuthenticationAsync));

            NuiManager.AttachNuiHandler("authenticate", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    Authenitcation authenitcation = new Authenitcation(metadata.Find<string>(0), metadata.Find<string>(1));
                    Rootobject result = await EventDispatcher.Get<Rootobject>("server:authenticate", Game.Player.ServerId, authenitcation);

                    if (result == null)
                    {
                        Logger.Error($"[ConnectionManager] Failed to authenticate. Please try again or contact a server admin.");
                        return new Dictionary<int, string>
                        {
                            { 0, "Failed to authenticate with the server." }
                        };
                    }

                    return result.citizens.ToDictionary(x => x.id, x => $"{x.name} {x.surname}");
                }
                catch (Exception ex)
                {
                    return new Dictionary<int, string>
                    {
                        { 0, "Failed to authenticate with the server." },
                        { 1, $"{ex.Message}" }
                    };
                }
            }));
        }

        //------------- Commented out as the method currently crashes the client ---------------------
        //private async Task<Dictionary<int, string>> OnAuthenticationAsync(Dictionary<string, string> data)
        //{
        //    Dictionary<int, string> result = new();
        //    try
        //    {
        //        CadAuthenitcation authenitcation = new CadAuthenitcation(data["0"], data["1"]);
        //        result = await EventDispatcher.Get<Dictionary<int, string>>("server:authenticate", Game.Player.ServerId, authenitcation);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return result;
        //    }
        //}

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
