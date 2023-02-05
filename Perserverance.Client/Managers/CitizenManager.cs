using Perserverance.Client.GameInterface;

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
                    CitizenMessage result = await EventDispatcher.Get<CitizenMessage>("server:getCitizens", Game.Player.ServerId, "", 0);

                    if (result == null)
                    {
                        Logger.Error($"[CitizenManager] Failed to get citizens. Please try again or contact a server admin");
                        return new CitizenMessage
                        {
                            errorMessage = "Failed to get citizens"
                        };
                    }

                    Logger.Debug($"[CitizenManager] Successfully got citizens");

                    return result;
                }
                catch (Exception ex)
                {
                    return new CitizenMessage
                    {
                        errorMessage = ex.Message
                    };
                }
            }));

            NuiManager.AttachNuiHandler("setCitizen", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    bool result = await EventDispatcher.Get<bool>("server:setCitizen", Game.Player.ServerId, metadata.Find<string>(0), metadata.Find<string>(1));

                    if (!result)
                    {
                        return new { success = false };
                    }

                    ConnectionManager.IsSpawned = true;

                    bool showLandingPage = GetResourceMetadata(GetCurrentResourceName(), "use_landing_page", 0) == "true";
                    bool showHudLandingPage = GetResourceMetadata(GetCurrentResourceName(), "enable_hud_landing_page_close", 0) == "true";

                    if (showLandingPage)
                    {
                        NuiManager.SetFocus(false, false);
                        NuiManager.SendMessage(new { action = "setLandingVisible", data = false });
                        
                        await Hud.FadeOut(1500);

                        await BaseScript.Delay(500);

                        Vector3 pos = new Vector3(1845f, 3633, 37f);

                        Game.PlayerPed.IsPositionFrozen = false;
                        Game.PlayerPed.IsInvincible = false;
                        Game.PlayerPed.HasGravity = true;

                        float groundZ = 0f;
                        bool gotGround = GetGroundZFor_3dCoord(pos.X, pos.Y, pos.Z, ref groundZ, false);

                        int failures = 0;

                        while (!gotGround)
                        {
                            if (failures > 100)
                                break;
                            
                            await BaseScript.Delay(100);
                            gotGround = GetGroundZFor_3dCoord(pos.X, pos.Y, pos.Z, ref groundZ, false);

                            failures++;
                        }

                        Game.PlayerPed.Position = new Vector3(pos.X, pos.Y, groundZ);

                        DisplayHud(showHudLandingPage);
                        DisplayRadar(showHudLandingPage);
                        
                        await BaseScript.Delay(1000);

                        RenderScriptCams(false, true, 0, false, false);
                        World.DestroyAllCameras();

                        await Hud.FadeIn(1500);
                    }

                    return new { success = true };
                }
                catch (Exception ex)
                {
                    return new CitizenMessage
                    {
                        errorMessage = ex.Message
                    };
                }
            }));
        }
    }
}
