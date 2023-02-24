namespace Perseverance.Shared.Models.SnailyCAD
{
    public class Errors
    {
        public string unknown { get; set; }
        public string userNotFound { get; set; }
        public string nameAlreadyTaken { get; set; }
        public string whitelistPending { get; set; }
        public string whitelistDeclined { get; set; }
        public string passwordIncorrect { get; set; }
        public string userAlreadyExists { get; set; }
        public string notFound { get; set; }
        public string plateAlreadyInUse { get; set; }
        public string invalidImageType { get; set; }
        public string userBanned { get; set; }
        public string userDeleted { get; set; }
        public string alreadyInThisBusiness { get; set; }
        public string insufficientPermissions { get; set; }
        public string maxBusinessesLength { get; set; }
        public string dateLargerThanNow { get; set; }
        public string divisionNotInDepartment { get; set; }
        public string passwordsDoNotMatch { get; set; }
        public string currentPasswordIncorrect { get; set; }
        public string citizenNotFound { get; set; }
        public string selectImageFirst { get; set; }
        public string officerIsCombined { get; set; }
        public string officerAlreadyMerged { get; set; }
        public string discordNameInUse { get; set; }
        public string discordAccountAlreadyLinked { get; set; }
        public string cannotRegisterFirstWithDiscord { get; set; }
        public string invalidRegistrationCode { get; set; }
        public string unitSuspended { get; set; }
        public string recordOrWarrantAlreadyLinked { get; set; }
        public string businessIsPending { get; set; }
        public string businessCreatedButPending { get; set; }
        public string maxDivisionsReached { get; set; }
        public string maxLimitOfficersPerUserReached { get; set; }
        public string maxCitizensPerUserReached { get; set; }
        public string discordIdInUse { get; set; }
        public string totpCodeRequired { get; set; }
        public string Error { get; set; }
        public string NOT_FOUND { get; set; }
        public string NetworkError { get; set; }
        public string mustSetBotTokenGuildId { get; set; }
        public string maxDepartmentsReachedPerUser { get; set; }
        public string allowRegularLoginIsDisabled { get; set; }
        public string InvalidPermissions { get; set; }
        public string alreadyPendingNameChange { get; set; }
        public string nameChangeRequestNotNew { get; set; }
        public string socketError { get; set; }
        public string socketErrorInfo { get; set; }
        public string fine_invalidDataReceived { get; set; }
        public string jailTime_invalidDataReceived { get; set; }
        public string bail_invalidDataReceived { get; set; }
        public string forbidden { get; set; }
        public string pageNotFound { get; set; }
        public string unitCallsignInUse { get; set; }
        public string steamNameInUse { get; set; }
        public string steamAccountAlreadyLinked { get; set; }
        public string cannotRegisterFirstWithSteam { get; set; }
        public string Unauthorized { get; set; }
        public string captchaRequired { get; set; }
        public string invalidCaptcha { get; set; }
        public string updateAvailable { get; set; }
        public string updateAvailableInfo { get; set; }
        public string serialNumberInUse { get; set; }
        public string vinNumberInUse { get; set; }
        public string alreadyHasPrimaryUnit { get; set; }
        public string noActiveOfficer { get; set; }
        public string socialSecurityNumberTaken { get; set; }
        public string citizenNotAllowedToEditLicenses { get; set; }
        public string vehicleIsImpounded { get; set; }
        public string featureNotEnabled { get; set; }
        public string errorUploadingImage { get; set; }
        public string noDefaultDepartmentSet { get; set; }
    }

}
