namespace Perseverance.Client.GameInterface.Events
{
    public class EventCallback
    {
        public Func<EventMetadata, object> Task { get; set; }

        public EventCallback(Func<EventMetadata, object> task)
        {
            Task = task;
        }
    }
}
