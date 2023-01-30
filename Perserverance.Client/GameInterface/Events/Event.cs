namespace Perserverance.Client.GameInterface.Events
{
    public enum EventType
    {
        Send,
        Request,
        Response
    }

    public class Event
    {
        [JsonProperty("__Seed")] public string Seed { get; set; }
        public string Target { get; set; }
        public int Sender { get; set; }
        public EventType Type { get; set; }
        public EventMetadata Metadata { get; set; }

        public Event()
        {
            Seed = Events.Seed.Generate();
            Type = EventType.Send;
            Metadata = new EventMetadata(this);
        }
    }
}
