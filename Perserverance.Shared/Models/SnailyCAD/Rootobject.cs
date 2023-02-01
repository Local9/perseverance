namespace Perserverance.Shared.Models.SnailyCAD
{
    [DataContract]
    public class EventMessage
    {
        [DataMember(Name = "success", EmitDefaultValue = false)]
        public bool Success { get; set; }
        
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "citizens", EmitDefaultValue = false)]
        public Citizen[] citizens { get; set; }

        [DataMember(Name = "totalCount", EmitDefaultValue = false)]
        public int totalCount { get; set; }
    }
}
