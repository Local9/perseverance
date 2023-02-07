namespace Perseverance.Client.Managers
{
    internal class KeybindManager : Manager<KeybindManager>
    {
        public static bool IsTabletOpen { get; private set; }

        public override void Begin()
        {
            RegisterKeyMapping("openTablet", "Open Tablet", "keyboard", "HOME");
            RegisterCommand("openTablet", new Action<int, List<object>, string>(OnOpenTablet), false);

            NuiManager.AttachNuiHandler("hideUI", new EventCallback(metadata =>
            {
                IsTabletOpen = false;
                NuiManager.SetFocus(false, false);
                return new EventMessage();
            }));
        }

        private void OnOpenTablet(int source, List<object> args, string rawCommand)
        {
            IsTabletOpen = true;
            NuiManager.SetFocus(true, true);
            NuiManager.SendMessage(new { action = "setTabletVisible", data = true });
        }
    }
}
