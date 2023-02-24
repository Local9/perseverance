namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Cad
    {
        public string id { get; set; }
        public string name { get; set; }
        public string areaOfPlay { get; set; }
        public int maxPlateLength { get; set; }
        public bool towWhitelisted { get; set; }
        public bool taxiWhitelisted { get; set; }
        public bool whitelisted { get; set; }
        public bool businessWhitelisted { get; set; }
        public Features features { get; set; }
        public object autoSetUserProperties { get; set; }
        public string registrationCode { get; set; }
        public string steamApiKey { get; set; }
        public string apiTokenId { get; set; }
        public ApiToken apiToken { get; set; }
        public MiscCadSettings miscCadSettings { get; set; }
        public string miscCadSettingsId { get; set; }
        public string logoId { get; set; }
        public object discordRolesId { get; set; }
        public object autoSetUserPropertiesId { get; set; }
        public Version version { get; set; }
    }

}
