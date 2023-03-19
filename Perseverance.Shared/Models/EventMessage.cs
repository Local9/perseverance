namespace Perseverance.Shared.Models
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
        public CitizenSelect citizen { get; set; }
        public List<CitizenSelect> citizens { get; set; }
        public int totalCount { get; set; }
    }
}
