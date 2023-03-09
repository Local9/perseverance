namespace Perseverance.Server.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getProps", new Func<EventSource, int, Task<List<PagePropsValue>>>(OnServerGetPropsAsync));
            EventDispatcher.Mount("server:getAddresses", new Func<EventSource, int, Task<List<Address>>>(OnServerGetAddressesAsync));
            EventDispatcher.Mount("server:searchAddresses", new Func<EventSource, int, string, Task<List<Address>>>(OnServerSearchAddressesAsync));
        }

        private async Task<List<Address>> OnServerSearchAddressesAsync([FromSource] EventSource source, int serverId, string query)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetAddressesAsync(source.User, query);
        }

        private async Task<List<Address>> OnServerGetAddressesAsync([FromSource] EventSource source, int serverId)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetAddressesAsync(source.User);
        }

        private async Task<List<PagePropsValue>> OnServerGetPropsAsync([FromSource] EventSource source, int serverId)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetServerSidePropertiesAsync(source.User);
        }
    }
}
