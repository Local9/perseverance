namespace Perserverance.Shared.Models.SnailyCAD
{
    public class Citizen
    {
        public string name { get; set; }
        public string surname { get; set; }
        public object imageId { get; set; }
        public object imageBlurData { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public string socialSecurityNumber { get; set; }
        public User user { get; set; }
    }

}
