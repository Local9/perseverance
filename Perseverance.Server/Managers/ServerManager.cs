namespace Perseverance.Server.Managers
{
    public class ServerManager : Manager<ServerManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getProps", new Func<EventSource, int, Task<List<PageProperty>>>(OnServerGetPropsAsync));
        }

        private async Task<List<PageProperty>> OnServerGetPropsAsync([FromSource] EventSource source, int serverId)
        {
            if (source.Handle != serverId) return null;

            return await AdminController.GetServerSideProperties(source.User);
        }
    }
}
