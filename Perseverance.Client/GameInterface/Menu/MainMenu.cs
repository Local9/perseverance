using Perseverance.Shared;
using ScaleformUI;

namespace Perseverance.Client.GameInterface.Menu
{
    internal class BaseMenu
    {
        public static UIMenu Menu { get; private set; }

        internal BaseMenu()
        {
            RegisterKeyMapping("+openMenu", "Open Menu", "keyboard", "");
            RegisterCommand("+openMenu", new Action(OpenMenuCommand), false);

            Menu = new UIMenu("Menu", "", Hud.MenuOffset)
            {
                EnableAnimation = false,
                MouseControlsEnabled = false,
                ControlDisablingEnabled = false,
                MouseWheelControlEnabled = true,
                BuildingAnimation = MenuBuildingAnimation.NONE,
                Glare = true
            };

            Menu.OnMenuStateChanged += BaseMenu_OnMenuStateChanged;

            Hud.MenuPool.Add(Menu);
            Main.Logger.Debug($"Initialised BaseMenu");
        }

        private void OpenMenuCommand()
        {
            if (Hud.MenuPool.IsAnyMenuOpen) return;
            Menu.Visible = true;
        }

        private void BaseMenu_OnMenuStateChanged(UIMenu oldMenu, UIMenu newMenu, MenuState state)
        {
            if (state.Equals(MenuState.Opened))
            {
                string characterName = Game.Player.State.Get(StateBagKey.CharacterName);
                string baseMenuTitle = Menu.Title;
                if (baseMenuTitle != characterName)
                {
                    Menu.Subtitle = characterName;
                }
            }
        }
    }
}
