namespace Perserverance.Server.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getCitizens", new Func<EventSource, int, string, int, Task<CitizenMessage>>(OnServerGetCitizensAsync));
        }

        /// <summary>
        /// Called when the client is requesting a list of citizens for the current user
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        private async Task<CitizenMessage> OnServerGetCitizensAsync([FromSource] EventSource source, int serverId, string query = "", int skip = 0)
        {
            try
            {
                if (source.Handle != serverId) return null;
                CitizenMessage result = await CitizenController.GetCitizens(source.User, query, skip);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"[CitizenManager] OnServerGetCitizensAsync() = >{ex.Message}");
                Logger.Error($"{ex}");
                return null;
            }
        }
    }
}
