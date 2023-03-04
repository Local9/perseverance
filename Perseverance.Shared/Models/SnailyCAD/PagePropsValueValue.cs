namespace Perseverance.Shared.Models.SnailyCAD
{
    public class PagePropsValueValue
    {
        public string id { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public bool isDefault { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int? position { get; set; }
        public bool isDisabled { get; set; }
        public object licenseType { get; set; }
        public object officerRankImageId { get; set; }
        public object officerRankImageBlurData { get; set; }
        public Count _count { get; set; }
    }

}
