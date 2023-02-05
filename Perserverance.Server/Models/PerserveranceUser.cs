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

        /// <summary>
        /// Sets the SnailyCAD authentication details for this user
        /// </summary>
        /// <param name="snailyAuthentication"></param>
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
