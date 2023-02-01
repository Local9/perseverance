namespace Perserverance.Client.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            NuiManager.AttachNuiHandler("getCitizens", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    BaseScript.TriggerServerEvent("testEvent");
                    CitizenMessage result = await EventDispatcher.Get<CitizenMessage>("server:getCitizens", Game.Player.ServerId, "", 0);

                    if (result == null)
                    {
                        Logger.Error($"[CitizenManager] Failed to get citizens. Please try again or contact a server admin");
                        return new CitizenMessage
                        {
                            success = false,
                            message = "Failed to get citizens"
                        };
                    }

                    Logger.Debug($"[CitizenManager] Successfully got citizens");

                    return result;
                }
                catch (Exception ex)
                {
                    return new CitizenMessage
                    {
                        success = false,
                        message = ex.Message
                    };
                }
            }));
        }
    }
}
