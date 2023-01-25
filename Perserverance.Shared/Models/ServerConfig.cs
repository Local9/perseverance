namespace Perserverance.Shared.Models
{
    [DataContract]
    public class ServerConfig
    {
        [DataMember(Name = "database")]
        public DatabaseConfig Database;

        [DataMember(Name = "discord")]
        public Discord Discord;
    }

    [DataContract]
    public class DatabaseConfig
    {
        [DataMember(Name = "application")]
        public string ApplicationName;

        [DataMember(Name = "server")]
        public string Server;

        [DataMember(Name = "databaseName")]
        public string DatabaseName;
        
        [DataMember(Name = "port")]
        public uint Port;

        [DataMember(Name = "username")]
        public string Username;

        [DataMember(Name = "password")]
        public string Password;

        [DataMember(Name = "minimumPoolSize")]
        // Minimum number of connections to the database by this connection
        public uint MinimumPoolSize { get; set; } = 10;

        [DataMember(Name = "maximumPoolSize")]
        // Maximum number of connections to the database by this connection
        public uint MaximumPoolSize { get; set; } = 50;

        [DataMember(Name = "connectionTimeout")]
        public uint ConnectionTimeout { get; set; } = 5;
    }

    [DataContract]
    public class Discord
    {
        [DataMember(Name = "botKey")]
        public string BotKey;

        [DataMember(Name = "botname")]
        public string Botname;

        [DataMember(Name = "webhooks")]
        public DiscordWebhooks Webhooks;

        [DataMember(Name = "whitelist")]
        public List<string> Whitelist;

        [DataMember(Name = "guildId")]
        public ulong GuildId;

        [DataMember(Name = "url")]
        public string Url;
    }

    [DataContract]
    public class DiscordWebhooks
    {
        [DataMember(Name = "server-debug")]
        public string ServerDebug;

        [DataMember(Name = "server-error")]
        public string ServerError;

        [DataMember(Name = "server-player-log")]
        public string ServerPlayerLog;
    }
}
