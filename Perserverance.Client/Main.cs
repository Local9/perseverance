using Logger;

namespace Perserverance.Client
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
            EventHandlers[eventName] += @delegate;
        }

        /// <summary>
        /// Attaches a Tick
        /// </summary>
        /// <param name="task"></param>
        public void AttachTickHandler(Func<Task> task)
        {
            Tick += task;
        }

        /// <summary>
        /// Detaches a Tick
        /// </summary>
        /// <param name="task"></param>
        public void DetachTickHandler(Func<Task> task)
        {
            Tick -= task;
        }
    }
}