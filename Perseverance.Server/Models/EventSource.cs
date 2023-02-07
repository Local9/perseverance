using FxEvents.Shared.EventSubsystem;

namespace Perseverance.Server.Models
{
    public partial class EventSource : ISource
    {
        public int Handle { get; set; }
        internal Player Player { get => Main.PlayerList[Handle]; }
        internal PerseveranceUser User { get; private set; }

        public EventSource()
        {

        }

        public EventSource(int handle)
        {
            Handle = handle;
            if (handle > 0)
                User = Main.ToPerserveranceUser(handle);
        }

        public override string ToString()
        {
            return $"{Handle} ({Player.Name})";
        }

        public static explicit operator EventSource(string netId)
        {
            if (int.TryParse(netId.Replace("net:", string.Empty), out int handle))
            {
                return new EventSource(handle);
            }

            throw new Exception($"Could not parse net id: {netId}");
        }

        public bool Compare(EventSource client)
        {
            return client.Handle == Handle;
        }

        public static explicit operator EventSource(int handle) => new(handle);
    }
}
