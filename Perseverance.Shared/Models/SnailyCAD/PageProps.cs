namespace Perseverance.Shared.Models.SnailyCAD
{
    public class PageProps
    {
        public PagePropsValue[] values { get; set; }
        public Session session { get; set; }
        public Messages messages { get; set; }
        public string _sentryTraceData { get; set; }
        public string _sentryBaggage { get; set; }
    }

}
