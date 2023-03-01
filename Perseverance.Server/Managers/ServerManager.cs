using Perseverance.Shared.Models.Generic;

namespace Perseverance.Server.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getProps", new Func<EventSource, int, Task<List<TypeList>>>(OnServerGetPropsAsync));
            EventDispatcher.Mount("server:getAddresses", new Func<EventSource, int, string, Task<List<TypeList>>>(OnServerGetAddressesAsync));
        }

        private async Task<List<TypeList>> OnServerGetAddressesAsync([FromSource] EventSource source, int serverId, string searchQuery)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetAddresses(source.User, searchQuery);
        }

        private async Task<List<TypeList>> OnServerGetPropsAsync([FromSource] EventSource source, int serverId)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetServerSideProperties(source.User);
        }
    }
}
