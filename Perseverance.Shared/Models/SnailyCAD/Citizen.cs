namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Citizen
    {
        public string name { get; set; } // first name, not full name

        public string surname { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object imageId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object imageBlurData { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        public string userId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string socialSecurityNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User user { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string fullname => $"{name} {surname}";
    }

}
