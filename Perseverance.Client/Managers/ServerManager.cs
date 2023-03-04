namespace Perseverance.Client.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("getServerProps", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                List<PagePropsValue> pageProperties = await EventDispatcher.Get<List<PagePropsValue>>("server:getProps", Game.Player.ServerId);
                result(pageProperties);
            }));

            RegisterNuiCallback("getAddresses", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                List<Address> pageProperties = await EventDispatcher.Get<List<Address>>("server:getAddresses", Game.Player.ServerId);
                result(pageProperties);
            }));
        }
    }
}
