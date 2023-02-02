namespace Perserverance.Server.SnailyCAD.Domain
{
    internal class SnailyCadAuthenticationDetails
    {
        public string UserId;
        public Dictionary<string, string> Cookies = new();
    }
}
