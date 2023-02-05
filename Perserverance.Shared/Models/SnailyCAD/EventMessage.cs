namespace Perserverance.Shared.Models.SnailyCAD
{
    public class EventMessage
    {
        public bool success
        {
            get
            {
                return string.IsNullOrEmpty(errorMessage);
            }
        }
        
        public string errorMessage { get; set; }
    }

    public class RegistrationMessage : EventMessage
    {
        public string whitelistStatus;
        public bool isOwner;
    }

    public class CitizenMessage : EventMessage
    {
        public List<Citizen> citizens { get; set; }
        public int totalCount { get; set; }
    }
}
