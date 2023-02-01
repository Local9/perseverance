namespace Perserverance.Server.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getCitizens", new Func<PerserveranceUser, int, string, int, Task<EventMessage>>(OnServerGetCitizensAsync));
        }

        /// <summary>
        /// Called when the client is requesting a list of citizens for the current user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="serverId"></param>
        /// <param name="query"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        private async Task<EventMessage> OnServerGetCitizensAsync([FromSource] PerserveranceUser user, int serverId, string query, int skip)
        {
            try
            {
                if (user.Handle != serverId) return null;
                Logger.Debug($"Player {user.Handle} is attempting to get citizens with query {query} and skip {skip}");

                EventMessage result = await CitizenController.GetCitizens(user, query, skip);

                Logger.Debug($"Player {user.Handle} has successfully gotten citizens with query {query} and skip {skip}");

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
    {
    }
}
