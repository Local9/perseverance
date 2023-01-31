using FxEvents.Shared.EventSubsystem;
using Perserverance.Server.SnailyCAD.Domain;

namespace Perserverance.Server.Models
{
    public partial class PerserveranceUser : ISource
    {
        public int Handle { get; set; }
        internal Player Player { get => Main.PlayerList[Handle]; }
        internal PerserveranceUser User { get; private set; }
        internal SnailyCadAuthenticationDetails SnailyAuth { get; private set; }

        public PerserveranceUser()
        {
            
        }

        public PerserveranceUser(int handle)
        {
            Handle = handle;
            if (handle > 0)
                User = Main.ToPerserveranceUser(handle);
        }

        internal void SetSnailyAuth(SnailyCadAuthenticationDetails snailyAuthentication)
        {
            SnailyAuth = snailyAuthentication;
        }

        public override string ToString()
        {
            return $"{Handle} ({Player.Name})";
        }

        public static explicit operator PerserveranceUser(string netId)
        {
            if (int.TryParse(netId.Replace("net:", string.Empty), out int handle))
            {
                return new PerserveranceUser(handle);
            }

            throw new Exception($"Could not parse net id: {netId}");
        }

        public bool Compare(PerserveranceUser client)
        {
            return client.Handle == Handle;
        }

        public static explicit operator PerserveranceUser(int handle) => new(handle);
    }
}
