using Perseverance.Client.GameInterface;

namespace Perseverance.Client.Managers
{
    public class ConnectionManager : Manager<ConnectionManager>
    {
        public static bool IsSpawned = false;

        const string DEFAULT_SCRIPTED_CAMERA = "DEFAULT_SCRIPTED_CAMERA";
        List<Camera> cameras = new();
        long gameTimer = 0;
        int cameraRotation = (1000 * 10);
        int cameraIndex = 0;

        public override void Begin()
        {
            Logger.Info($"[ConnectionManager] Started");
            Event("onResourceStart", new Action<string>(OnResourceStartAsync));
        }

        /// <summary>
        /// Called when the resource starts on the client
        /// </summary>
        /// <param name="resourceName"></param>
        private async void OnResourceStartAsync(string resourceName)
        {
            Hud.StartLoadingMessage("PM_WAIT");

            if (resourceName != GetCurrentResourceName()) return;

            EventMessage eventMessage = await EventDispatcher.Get<EventMessage>("connection:active", Game.Player.ServerId);
            Session.IsSessionReady = eventMessage.success;
            bool showLandingPage = GetResourceMetadata(GetCurrentResourceName(), "use_landing_page", 0) == "true";

            if (Session.IsSessionReady && showLandingPage)
            {
                NuiManager.SetFocus(true, true);
                NuiManager.SendMessage(new { action = "setLandingVisible", data = true });

                Camera camera = CreateCamera(new Vector3(-1021f, -471f, 37f), new Vector3(10, 0, 100), 50f, 7f, 1f, 1.2f, 1f); // Movie
                cameras.Add(camera);
                Camera camera2 = CreateCamera(new Vector3(389f, -1009f, 41f), new Vector3(-10, 0, 287), 50f, 7f, 1f, 1.2f, 1f); // Mission Row
                cameras.Add(camera2);
                Camera camera3 = CreateCamera(new Vector3(1845f, 3633, 37f), new Vector3(-5, 0, 8), 50f, 7f, 1f, 1.2f, 1f); // Sandy
                cameras.Add(camera3);

                World.RenderingCamera = camera;

                gameTimer = GetGameTimer();

                Game.PlayerPed.IsInvincible = true;
                Game.PlayerPed.HasGravity = false;
                Game.PlayerPed.Position = camera.Position + new Vector3(0f, 0f, 2f);
                Game.PlayerPed.IsPositionFrozen = true;

                DisplayHud(false);
                DisplayRadar(false);

                Instance.AttachTickHandler(OnAmbientCameraAsync);
            }
            else
            {
                NetworkConcealPlayer(Game.Player.Handle, false, false);
                DisplayHud(true);
                DisplayRadar(true);

                Hud.CloseLoadingScreen();
            }

            Hud.StopLoadingMessage();

            Logger.Debug("Perseverance.Client has started!");
        }

        /// <summary>
        /// rotates between ambient cameras
        /// </summary>
        /// <returns></returns>
        private async Task OnAmbientCameraAsync()
        {
            if (IsSpawned)
            {
                Instance.DetachTickHandler(OnAmbientCameraAsync);
                return;
            }

            if (GetGameTimer() - gameTimer < cameraRotation) return;
            await Hud.FadeOut(1500);

            cameraIndex++;
            if (cameraIndex > cameras.Count)
                cameraIndex = 0;
            
            await BaseScript.Delay(100);
            Camera nextCamera = cameras[cameraIndex];
            World.RenderingCamera = nextCamera;
            Vector3 pos = nextCamera.Position;
            Game.PlayerPed.Position = pos + new Vector3(0f, 0f, 2f);
            PopulateNow();

            // get random cloud hat and set it
            if (World.Weather == Weather.Clouds)
            {
                Array cloudHats = Enum.GetValues(typeof(CloudHat));
                World.CloudHat = (CloudHat)cloudHats.GetValue(Main.Random.Next(cloudHats.Length));
            }

            float groundZ = 0f;

            bool gotGround = GetGroundZFor_3dCoord(pos.X, pos.Y, pos.Z, ref groundZ, false);

            while (!gotGround)
            {
                await BaseScript.Delay(100);
                gotGround = GetGroundZFor_3dCoord(pos.X, pos.Y, pos.Z, ref groundZ, false);
            }

            await BaseScript.Delay(1000);

            await Hud.FadeIn(1500);
            gameTimer = GetGameTimer();
        }

        private Camera CreateCamera(Vector3 position, Vector3 roation, float fieldOfView, 
            float dofFocusDistance = 0, float dofFocusDistanceBlend = 0, float dofLens = 0, float dofNear = 0)
        {
            Camera camera = new Camera(CreateCam(DEFAULT_SCRIPTED_CAMERA, true));
            camera.Position = position;
            camera.Rotation = roation;
            camera.FieldOfView = fieldOfView;
            camera.StopShaking();

            if (dofFocusDistance > 0)
                N_0xf55e4046f6f831dc(camera.Handle, dofFocusDistance); // SET_CAM_DOF_OVERRIDDEN_FOCUS_DISTANCE

            if (dofFocusDistanceBlend > 0)
                N_0xe111a7c0d200cbc5(camera.Handle, dofFocusDistanceBlend); // SET_CAM_DOF_OVERRIDDEN_FOCUS_DISTANCE_BLEND_LEVEL

            if (dofLens > 0)
                SetCamDofFnumberOfLens(camera.Handle, dofLens);

            if (dofNear > 0)
                SetCamDofMaxNearInFocusDistanceBlendLevel(camera.Handle, dofNear);

            return camera;
        }
    }
}
