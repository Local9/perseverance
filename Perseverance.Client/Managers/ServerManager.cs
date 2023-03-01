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

            RegisterNuiCallback("getAddresses", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                string searchQuery = string.Empty;

                if (body.ContainsKey("query"))
                {
                    searchQuery = body["query"].ToString();
                }

                List<TypeList> pageProperties = await EventDispatcher.Get<List<TypeList>>("server:getAddresses", Game.Player.ServerId, searchQuery);
                result(pageProperties);
            }));
        }
    }
}
