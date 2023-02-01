namespace Perserverance.Shared.Models.SnailyCAD
{
    public class EventMessage
    {
        public bool success { get; set; } = true;
        public string message { get; set; }
    }

    public class CitizenMessage : EventMessage
    {
        public List<Citizen> citizens { get; set; }
        public int totalCount { get; set; }
    }
}
