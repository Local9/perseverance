using System.Security.Cryptography;

namespace Perserverance.Server.Models.Config
{
    [DataContract]
    public class ServerConfig
    {
        [DataMember(Name = "database")]
        public DatabaseConfig Database;

        [DataMember(Name = "discord")]
        public Discord Discord;

        [DataMember(Name = "snailycad")]
        public SnailyCad SnailyCad;
    }
}
