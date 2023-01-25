using Logger;
using Perserverance.Shared.Models;

namespace Perserverance.Server
{
    static class ServerConfiguration
    {
        const string SERVER_CONFIG_LOCATION = $"/data/server-config.json";
        private static ServerConfig _serverConfig = null;

        private static ServerConfig GetConfig()
        {
            try
            {
                if (_serverConfig is not null)
                    return _serverConfig;

                string serverConfigFile = LoadResourceFile(GetCurrentResourceName(), SERVER_CONFIG_LOCATION);
                _serverConfig = JsonConvert.DeserializeObject<ServerConfig>(serverConfigFile);
                return _serverConfig;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{Log.DARK_RED}Server Configuration was unable to be loaded.");
                Debug.WriteLine($"{ex}");
                return (ServerConfig)default!;
            }
        }

        public static ServerConfig GetServerConfig => GetConfig();
        public static DatabaseConfig GetDatabaseConfig => GetServerConfig.Database;
        public static Discord GetDiscordConfig => GetServerConfig.Discord;
    }
}
