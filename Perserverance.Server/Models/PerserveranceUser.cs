namespace Perserverance.Server.Models
{
    public class PerserveranceUser
    {
        public int Handle { get; set; }
        internal Player Player { get => Main.PlayerList[Handle]; }
        internal SnailyCadAuthenticationDetails SnailyAuth { get; private set; }

        public PerserveranceUser(int handle)
        {
            Handle = handle;
            SnailyAuth = new SnailyCadAuthenticationDetails();
        }

        internal void SetSnailyAuth(SnailyCadAuthenticationDetails snailyAuthentication)
        {
            SnailyAuth = snailyAuthentication;
        }

        public override string ToString()
        {
            return $"{Handle} ({Player.Name})";
        }
    }
}
