namespace Perseverance.Server.Models.Config
{
    [DataContract]
    public class SnailyCad
    {
        [DataMember(Name = "url")]
        public string Url;

        [DataMember(Name = "apikey")]
        public string ApiKey;
    }
}
