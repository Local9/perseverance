namespace Perserverance.Server.Models.Config
{
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
}
