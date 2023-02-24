namespace Perseverance.Shared.Models.SnailyCAD
{
    public class ApiToken
    {
        public string id { get; set; }
        public bool enabled { get; set; }
        public string token { get; set; }
        public object[] routes { get; set; }
        public object uses { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

}
