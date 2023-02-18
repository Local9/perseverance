namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Registration
    {
        [JsonProperty(Required = Required.Always)]
        public string username { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string password { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string confirmPassword { get; set; }

        public string registrationCode { get; set; }
        public object captchaResult { get; set; }
    }

}
