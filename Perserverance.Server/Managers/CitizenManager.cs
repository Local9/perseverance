using FxEvents.Shared.EventSubsystem;
using Perserverance.Shared;

namespace Perserverance.Server.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getCitizens", new Func<EventSource, int, string, int, Task<CitizenMessage>>(OnServerGetCitizensAsync));
            EventDispatcher.Mount("server:setCitizen", new Func<EventSource, int, string, string, Task<bool>>(OnServerSetCitizen));
        }

        private async Task<bool> OnServerSetCitizen([FromSource] EventSource source, int serverId, string citizenId, string citizenFullname)
        {
            if (source.Handle != serverId) return false;
            source.Player.State.Set(StateBagKey.CharacterName, citizenFullname, true);

            bool showLandingPage = GetResourceMetadata(GetCurrentResourceName(), "use_landing_page", 0) == "true";

            if (showLandingPage)
            {
                int defaultBucket = 0;
                string str = GetResourceMetadata(GetCurrentResourceName(), "default_player_bucket", 0);
                
                if (!string.IsNullOrEmpty(str))
                    int.TryParse(str, out defaultBucket);

                SetPlayerRoutingBucket($"{source.Handle}", defaultBucket);
            }

            // Believe the API needs to be told something or does the active citizen need storing on the player?

            return true;
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
