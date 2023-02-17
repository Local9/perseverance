namespace Perseverance.Client.Managers
{
    public class KeybindManager : Manager<KeybindManager>
    {
        public static bool IsTabletOpen { get; private set; }
        const string COMMAND_OPEN_TABLET = "+openTablet";

        public override void Begin()
        {
            // HASH ~INPUT_6D37387D~
            RegisterKeyMapping(COMMAND_OPEN_TABLET, "Open Tablet", "keyboard", "HOME");
            RegisterCommand(COMMAND_OPEN_TABLET, new Action(OnOpenTablet), false);

            RegisterNuiCallback("hideTabletUI", new Action<IDictionary<string, object>, CallbackDelegate>((body, result) =>
            {
                IsTabletOpen = false;
                NuiManager.SetFocus(false, false);
                result(new { success = true });
            }));
        }

        private void OnOpenTablet()
        {
            IsTabletOpen = true;
            NuiManager.SetFocus(true, true);
            NuiManager.SendMessage(new { action = "setTabletVisible", data = true });
        }
    }
}
