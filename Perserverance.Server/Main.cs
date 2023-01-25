using Logger;

namespace Perserverance.Server
{
    public class Main : BaseScript
    {
        public static Main Instance { get; private set; }
        public static Log Logger { get; private set; }
        public static PlayerList PlayerList { get; private set; }

        public Main()
        {
            Instance = this;
            Logger = new();
            PlayerList = Players;

            Load();
        }

        private async void Load()
        {
            bool databaseTest = await Database.DapperDatabase<bool>.GetSingleAsync("select 1;");
            if (databaseTest)
            {
                Logger.Info($"Database Connection Test Successful!");
            }
            else
            {
                Logger.Error($"Database Connection Test Failed!");
            }
        }

        /// <summary>
        /// Adds an event handler to the event handlers dictionary.
        /// </summary>
        /// <remarks>This event will not go through FxEvents</remarks>
        /// <param name="eventName"></param>
        /// <param name="delegate"></param>
        public void AddEventHandler(string eventName, Delegate @delegate)
        {
            Logger.Debug($"Registered Event Handler '{eventName}'");
            EventHandlers.Add(eventName, @delegate);
        }
    }
}