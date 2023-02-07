namespace Perseverance.Server.SnailyCAD.Domain
{
    internal class SnailyCadAuthenticationDetails
    {
        public string UserId;
        public Dictionary<string, string> Cookies = new();
        public bool IsOwner;
        public WhitelistStatus WhitelistStatus;
    }

    internal enum WhitelistStatus
    {
        UNKNOWN,
        ACCEPTED,
        PENDING,
        DECLINED
    }
}
