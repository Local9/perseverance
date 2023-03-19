using Perseverance.Shared;

namespace Perseverance.Server.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            EventDispatcher.Mount("server:getCitizen", new Func<EventSource, int, string, Task<CitizenMessage>>(OnServerGetCitizenAsync));
            EventDispatcher.Mount("server:getCitizens", new Func<EventSource, int, string, int, Task<CitizenMessage>>(OnServerGetCitizensAsync));
            EventDispatcher.Mount("server:setCitizen", new Func<EventSource, int, string, string, Task<bool>>(OnServerSetCitizen));
            EventDispatcher.Mount("server:saveCitizen", new Func<EventSource, int, CitizenCreate, Task<CitizenMessage>>(OnServerSaveCitizen));

            Export.Add("getActiveCitizen", new Func<int, string>(OnGetActiveCitizen));
        }

        private async Task<CitizenMessage> OnServerSaveCitizen([FromSource] EventSource source, int serverId, CitizenCreate citizen)
        {
            if (source.Handle != serverId) return null;

            if (string.IsNullOrEmpty(citizen.id))
                return await CitizenController.CreateAsync(source.User, citizen);

            return await CitizenController.UpdateAsync(source.User, citizen);
        }

        private string OnGetActiveCitizen(int source)
        {
            PerseveranceUser player = Main.ToPerserveranceUser(source);
            return player.Citizen.ToJson();
        }

        /// <summary>
        /// Called when the client is setting the active citizen for the current user
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="citizenId"></param>
        /// <param name="citizenFullname"></param>
        /// <returns></returns>
        private Task<bool> OnServerSetCitizen([FromSource] EventSource source, int serverId, string citizenId, string citizenFullname)
        {
            if (source.Handle != serverId) return Task.FromResult(false);
            bool result = source.User.SetCitizen(citizenId);

            if (!result)
            {
                return Task.FromResult(false);
            }

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

            return Task.FromResult(true);
        }

        /// <summary>
        /// Called when the client is requesting a list of citizens for the current user
        /// </summary>
        /// <param name="source"></param>
        /// <param name="serverId"></param>
        /// <param name="citizenId"></param>
        /// <returns></returns>
        private async Task<CitizenMessage> OnServerGetCitizenAsync([FromSource] EventSource source, int serverId, string citizenId = "")
        {
            try
            {
                if (source.Handle != serverId) return null;
                CitizenMessage result = await CitizenController.GetCitizenAsync(source.User, citizenId);

                source.User.SetCitizens(result.citizens);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"[CitizenManager] OnServerGetCitizenAsync() = >{ex.Message}");
                Logger.Error($"{ex}");
                return null;
            }
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
                CitizenMessage result = await CitizenController.GetCitizensAsync(source.User, query, skip);

                source.User.SetCitizens(result.citizens);

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
