namespace Perseverance.Shared.Models.SnailyCAD
{
    public class MiscCadSettings
    {
        public string id { get; set; }
        public object cadOGDescription { get; set; }
        public string heightPrefix { get; set; }
        public string weightPrefix { get; set; }
        public int maxCitizensPerUser { get; set; }
        public int maxOfficersPerUser { get; set; }
        public int maxPlateLength { get; set; }
        public object maxBusinessesPerCitizen { get; set; }
        public object maxDivisionsPerOfficer { get; set; }
        public object maxDepartmentsEachPerUser { get; set; }
        public object maxAssignmentsToIncidents { get; set; }
        public object maxAssignmentsToCalls { get; set; }
        public string callsignTemplate { get; set; }
        public object caseNumberTemplate { get; set; }
        public string pairedUnitTemplate { get; set; }
        public string pairedUnitSymbol { get; set; }
        public bool signal100Enabled { get; set; }
        public string liveMapURL { get; set; }
        public bool roleplayEnabled { get; set; }
        public object authScreenBgImageId { get; set; }
        public string authScreenHeaderImageId { get; set; }
        public object inactivityTimeout { get; set; }
        public object jailTimeScale { get; set; }
        public object call911InactivityTimeout { get; set; }
        public object incidentInactivityTimeout { get; set; }
        public object unitInactivityTimeout { get; set; }
        public object boloInactivityTimeout { get; set; }
        public object activeWarrantsInactivityTimeout { get; set; }
        public object activeDispatchersInactivityTimeout { get; set; }
        public int driversLicenseNumberLength { get; set; }
        public object driversLicenseTemplate { get; set; }
        public int pilotLicenseNumberLength { get; set; }
        public object pilotLicenseTemplate { get; set; }
        public int weaponLicenseNumberLength { get; set; }
        public object weaponLicenseTemplate { get; set; }
        public int waterLicenseNumberLength { get; set; }
        public object waterLicenseTemplate { get; set; }
        public DateTime lastInactivitySyncTimestamp { get; set; }
        public object call911WebhookId { get; set; }
        public object statusesWebhookId { get; set; }
        public object panicButtonWebhookId { get; set; }
        public object boloWebhookId { get; set; }
        public object[] webhooks { get; set; }
    }

}
