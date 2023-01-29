using FxEvents;

namespace Perserverance.Client.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public override void Begin()
        {
            Logger.Info($"[ConnectionManager] Started");
            Event("onResourceStart", new Action<string>(OnResourceStartAsync));
        }

        private async void OnResourceStartAsync(string resourceName)
        {
            if (resourceName != GetCurrentResourceName()) return;

            Session.IsSessionReady = await EventDispatcher.Get<bool>("connection:active", Game.Player.ServerId);

            Logger.Debug("Perserverance.Client has started!");
        }
    }
}
