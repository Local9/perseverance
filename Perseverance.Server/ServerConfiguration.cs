using Perseverance.Server.Models.Config;

namespace Perseverance.Server
{
    internal class ServerConfiguration
    {
        const string SERVER_CONFIG_LOCATION = $"/data/server-config.json";
        ServerConfig serverConfig = null;

        ServerConfig GetConfig()
        {
            try
            {
                string serverConfigFile = LoadResourceFile(GetCurrentResourceName(), SERVER_CONFIG_LOCATION);
                serverConfig = JsonConvert.DeserializeObject<ServerConfig>(serverConfigFile);
                return serverConfig;
            }
            catch (Exception ex)
            {
                Main.Logger.Error($"Server Configuration was unable to be loaded.");
                Main.Logger.Info($"{ex}");
                Main.Logger.Error($"---------------------------------------------.");
                return (ServerConfig)default!;
            }
        }

        internal ServerConfig GetServerConfig => serverConfig ??= GetConfig();
        internal DatabaseConfig GetDatabaseConfig => GetServerConfig.Database;
        internal Discord GetDiscordConfig => GetServerConfig.Discord;
    }
}
