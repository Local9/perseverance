using Perseverance.Shared.Models.Generic;

namespace Perseverance.Client.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("getServerProps", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                List<TypeList> pageProperties = await EventDispatcher.Get<List<TypeList>>("server:getProps", Game.Player.ServerId);
                result(pageProperties);
            }));
        }
    }
}
