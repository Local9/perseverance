namespace Perseverance.Client.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("getServerProps", new Action<IDictionary<string, object>, CallbackDelegate>(OnServerGetPropsAsync));
            RegisterNuiCallback("getAddresses", new Action<IDictionary<string, object>, CallbackDelegate>(OnServerGetAddressesAsync));
        }

        private async void OnServerGetPropsAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            List<PagePropsValue> pageProperties = await EventDispatcher.Get<List<PagePropsValue>>("server:getProps", Game.Player.ServerId);
            result(pageProperties);
        }

        private async void OnServerGetAddressesAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            List<Address> addresses = await EventDispatcher.Get<List<Address>>("server:getAddresses", Game.Player.ServerId);
            result(addresses);
        }
    }
}
