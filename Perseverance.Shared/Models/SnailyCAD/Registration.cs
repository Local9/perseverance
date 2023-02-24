namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Registration
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string registrationCode { get; set; }
        public object captchaResult { get; set; }
    }

}
