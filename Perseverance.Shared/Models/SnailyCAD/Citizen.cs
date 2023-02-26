namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Citizen
    {
        public string id { get; set; }
        public string citizen { get; set; }
        public string citizens { get; set; }
        public string userNoCitizens { get; set; }
        public string createCitizen { get; set; }
        public string viewCitizen { get; set; }
        public string editCitizen { get; set; }
        public string editingCitizen { get; set; }
        public string deleteCitizen { get; set; }
        public string hairColor { get; set; }
        public string eyeColor { get; set; }
        public string registerVehicle { get; set; }
        public string registerWeapon { get; set; }
        public string createTowCall { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string ethnicity { get; set; }
        public string address { get; set; }
        public string dateOfBirth { get; set; }
        public string image { get; set; }
        public string age { get; set; }
        public string driversLicense { get; set; }
        public string driversLicenseCategory { get; set; }
        public string weaponLicense { get; set; }
        public string phoneNumber { get; set; }
        public string pilotLicense { get; set; }
        public string pilotLicenseCategory { get; set; }
        public string waterLicense { get; set; }
        public string waterLicenseCategory { get; set; }
        public string firearmLicenseCategory { get; set; }
        public string ccw { get; set; }
        public string createTaxiCall { get; set; }
        public string create911Call { get; set; }
        public string socialSecurityNumber { get; set; }
        public string occupation { get; set; }
        public string additionalInfo { get; set; }
        public string manageOccupation { get; set; }
        public string manageLicenses { get; set; }
        public string licenses { get; set; }
        public string licenseSuspendedInfo { get; set; }
        public string licenseNumber { get; set; }
        public string unmarkAsStolen { get; set; }
        public string markCitizenDeceased { get; set; }
        public string citizenDead { get; set; }
        public string basicInformation { get; set; }
        public string optionalInformation { get; set; }
        public string licenseInformation { get; set; }
        public string officer { get; set; }
        public string createWithOfficer { get; set; }
        public string previousRecords { get; set; }
        public string addressFlags { get; set; }
        public string createPreviousRecordsStepDescription { get; set; }
        public string alert_markCitizenDeceased { get; set; }
        public string alert_deleteCitizen { get; set; }
        public User user { get; set; }
        public string fullname => $"{name} {surname}";
    }

}
