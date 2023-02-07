namespace Perserverance.Shared.Models.SnailyCAD
{
    public class UserBase
    {
        public object apiToken { get; set; }
        public object apiTokenId { get; set; }
        public object avatarUrl { get; set; }
        public bool banned { get; set; }
        public object banReason { get; set; }
        public DateTime createdAt { get; set; }
        public string discordId { get; set; }
        public string id { get; set; }
        public bool isDarkTheme { get; set; }
        public bool isDispatch { get; set; }
        public bool isEmsFd { get; set; }
        public bool isLeo { get; set; }
        public bool isSupervisor { get; set; }
        public bool isTaxi { get; set; }
        public bool isTow { get; set; }
        public object lastDiscordSyncTimestamp { get; set; }
        public DateTime lastSeen { get; set; }
        public object locale { get; set; }
        public object[] permissions { get; set; }
        public string rank { get; set; }
        public object[] roles { get; set; }
        public object soundSettings { get; set; }
        public object soundSettingsId { get; set; }
        public string statusViewMode { get; set; }
        public string steamId { get; set; }
        public string tableActionsAlignment { get; set; }
        public object tempPassword { get; set; }
        public DateTime updatedAt { get; set; }
        public string username { get; set; }
        public string whitelistStatus { get; set; }
    }
}