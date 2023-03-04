﻿namespace Perseverance.Client.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("getServerProps", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                List<PageProperty> pageProperties = await EventDispatcher.Get<List<PageProperty>>("server:getProps", Game.Player.ServerId);
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
