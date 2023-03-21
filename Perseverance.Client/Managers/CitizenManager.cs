using FxEvents.Shared;
using Perseverance.Client.GameInterface;

namespace Perseverance.Client.Managers
{
    public class CitizenManager : Manager<CitizenManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("getCitizen", new Action<IDictionary<string, object>, CallbackDelegate>(OnGetCitizenAsync));
            RegisterNuiCallback("getCitizens", new Action<IDictionary<string, object>, CallbackDelegate>(OnGetCitizensAsync));
            RegisterNuiCallback("setCitizen", new Action<IDictionary<string, object>, CallbackDelegate>(OnSetCitizenAsync));
            RegisterNuiCallback("saveCitizen", new Action<IDictionary<string, object>, CallbackDelegate>(OnSaveCitizenAsync));
            RegisterNuiCallback("deleteCitizen", new Action<IDictionary<string, object>, CallbackDelegate>(OnDeleteCitizenAsync));
        }

        private async void OnDeleteCitizenAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                CitizenMessage eventMessage = await EventDispatcher.Get<CitizenMessage>("server:deleteCitizen", Game.Player.ServerId, body["id"]);

                if (eventMessage == null)
                {
                    Logger.Error($"[CitizenManager] Failed to get citizen. Please try again or contact a server admin");
                    result(new CitizenMessage
                    {
                        errorMessage = "Failed to get citizen"
                    });
                }

                Logger.Debug($"[CitizenManager] Successfully got citizen");

                result(eventMessage);
            }
            catch (Exception ex)
            {
                result(new EventMessage
                {
                    errorMessage = ex.Message
                });
            }
        }

        private async void OnGetCitizenAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                CitizenMessage eventMessage = await EventDispatcher.Get<CitizenMessage>("server:getCitizen", Game.Player.ServerId, body["id"]);

                if (eventMessage == null)
                {
                    Logger.Error($"[CitizenManager] Failed to get citizen. Please try again or contact a server admin");
                    result(new CitizenMessage
                    {
                        errorMessage = "Failed to get citizen"
                    });
                }

                Logger.Debug($"[CitizenManager] Successfully got citizen");

                result(eventMessage);
            }
            catch (Exception ex)
            {
                result(new EventMessage
                {
                    errorMessage = ex.Message
                });
            }
        }

        private async void OnGetCitizensAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                CitizenMessage eventMessage = await EventDispatcher.Get<CitizenMessage>("server:getCitizens", Game.Player.ServerId, "", 0);

                if (eventMessage == null)
                {
                    Logger.Error($"[CitizenManager] Failed to get citizens. Please try again or contact a server admin");
                    result(new CitizenMessage
                    {
                        errorMessage = "Failed to get citizens"
                    });
                }

                Logger.Debug($"[CitizenManager] Successfully got citizens");

                result(eventMessage);
            }
            catch (Exception ex)
            {
                result(new EventMessage
                {
                    errorMessage = ex.Message
                });
            }
        }

        private async void OnSetCitizenAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                Dictionary<string, string> keyValuePairs = body.ToDictionary(x => x.Key, x => x.Value.ToString());

                bool success = await EventDispatcher.Get<bool>("server:setCitizen", Game.Player.ServerId, keyValuePairs["id"], keyValuePairs["fullname"]);

                if (!success)
                {
                    result(new { success = false });
                    return;
                }

                ConnectionManager.IsSpawned = true;

                bool showLandingPage = GetResourceMetadata(GetCurrentResourceName(), "use_landing_page", 0) == "true";

                if (showLandingPage)
                {
                    NuiManager.SetFocus(false, false);

                    await Hud.FadeOut(1500);

                    await BaseScript.Delay(500);

                    Vector3 pos = new Vector3(1845f, 3633, 37f);

                    Game.PlayerPed.IsPositionFrozen = false;
                    Game.PlayerPed.IsInvincible = false;
                    Game.PlayerPed.HasGravity = true;
                    Game.PlayerPed.IsVisible = true;

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

                    DisplayHud(true);
                    DisplayRadar(true);

                    await BaseScript.Delay(1000);

                    RenderScriptCams(false, true, 0, false, false);
                    World.DestroyAllCameras();

                    await Hud.FadeIn(1500);
                }

                NewLoadSceneStop();
                NetworkStopLoadScene();

                Screen.DisplayHelpTextThisFrame($"Press ~INPUT_6D37387D~ to open the SnailyCAD Tablet");

                result(new { success = true });
            }
            catch (Exception ex)
            {
                result(new CitizenMessage
                {
                    errorMessage = ex.Message
                });
            }
        }

        private async void OnSaveCitizenAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                CitizenCreate citizen = JsonConvert.DeserializeObject<CitizenCreate>(body.ToJson());

                CitizenMessage citizenMessage = await EventDispatcher.Get<CitizenMessage>("server:saveCitizen", Game.Player.ServerId, citizen);

                result(citizenMessage);
            }
            catch (Exception ex)
            {
                result(new CitizenMessage
                {
                    errorMessage = ex.Message
                });
            }
        }
    }
}
