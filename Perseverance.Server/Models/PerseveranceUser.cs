namespace Perseverance.Server.Models
{
    public class PerseveranceUser
    {
        public int Handle { get; set; }
        internal Player Player { get => Main.PlayerList[Handle]; }
        internal SnailyCadAuthenticationDetails SnailyAuth { get; private set; }
        internal Citizen Citizen { get; private set; }
        internal List<CitizenSelect> Citizens { get; private set; }

        public PerseveranceUser(int handle)
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

        internal bool SetCitizen(string citizenId)
        {
            CitizenSelect citizen = Citizens.Where(x => x.id == citizenId).FirstOrDefault();

            if (citizen is null)
            {
                Main.Logger.Error($"Could not find citizen with id {citizenId} for user {Handle}");
                return false;
            }

            Citizen = citizen;
            return true;
        }

        internal void SetCitizens(List<CitizenSelect> citizens)
        {
            Citizens = citizens;
        }

        public override string ToString()
        {
            return $"{Handle} ({Player.Name})";
        }
    }
}
