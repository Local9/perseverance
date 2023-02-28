using Logger;
using Perseverance.Client.GameInterface;
using Perseverance.Client.Managers;
using System.Reflection;

namespace Perseverance.Client
{
    public class Main : BaseScript
    {
        internal static Main Instance { get; private set; }
        internal static Log Logger { get; private set; }
        internal static Hud Hud { get; private set; }
        internal static NuiManager NuiManager { get; private set; }
        internal static PlayerList PlayerList { get; private set; }
        internal static Random Random = new Random(DateTime.UtcNow.Millisecond);
        public static int GameTime { get; private set; }
        public Dictionary<Type, object> Managers { get; } = new();
        public Dictionary<Type, List<MethodInfo>> TickHandlers { get; set; } = new();
        public List<Type> RegisteredTickHandlers { get; set; } = new();

        public Main()
        {
            Instance = this;
            Logger = new();
            Hud = new();
            NuiManager = new();
            PlayerList = Players;

            LoadManagers();
        }

        internal void LoadManagers()
        {
            try
            {
                List<MethodInfo> managers = Assembly.GetExecutingAssembly().GetExportedTypes()
                    .SelectMany(self => self.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                    .Where(self => self.GetCustomAttribute(typeof(TickHandler), false) != null).ToList();

                managers.ForEach(self =>
                {
                    try
                    {
                        var type = self.DeclaringType;
                        if (type == null) return;

                        Logger.Debug($"[TickHandlers] {type.Name}::{self.Name}");

                        if (!TickHandlers.ContainsKey(type))
                        {
                            TickHandlers.Add(type, new List<MethodInfo>());
                        }

                        TickHandlers[type].Add(self);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{ex}");
                        BaseScript.TriggerServerEvent("user:log:exception", $"Error with Tick; {ex.Message}", ex);
                    }
                });

                var loaded = 0;

                foreach (var type in Assembly.GetExecutingAssembly().GetExportedTypes())
                {
                    if (type.BaseType == null) continue;
                    if (!type.BaseType.IsGenericType) continue;

                    var generic = type.BaseType.GetGenericTypeDefinition();

                    if (generic != typeof(Manager<>) || type == typeof(Manager<>)) continue;

                    LoadManager(type);

                    loaded++;
                }

                foreach (var manager in Managers)
                {
                    var method = manager.Key.GetMethod("Begin", BindingFlags.Public | BindingFlags.Instance);
                    method?.Invoke(manager.Value, null);
                }

                Logger.Info($"[Managers] Successfully loaded in {loaded} manager(s)!");

                AttachTickHandlers(this);

                Logger.Info("Load method has been completed.");
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex}");
            }
        }

        internal object LoadManager(Type type)
        {
            if (GetManager(type) != null) return null;

            Logger.Debug($"Loading in manager of type `{type.FullName}`");

            Managers.Add(type, default(Type));

            var instance = Activator.CreateInstance(type);

            AttachTickHandlers(instance);
            Managers[type] = instance;

            return instance;
        }

        internal void AttachTickHandlers(object instance)
        {
            TickHandlers.TryGetValue(instance.GetType(), out var methods);

            methods?.ForEach(async self =>
            {
                try
                {
                    var handler = (TickHandler)self.GetCustomAttribute(typeof(TickHandler));

                    if (handler.SessionWait)
                    {
                        await Session.IsReady();
                    }

                    Logger.Debug($"AttachTickHandlers -> {self.Name}");

                    Tick += (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), instance, self);

                    RegisteredTickHandlers.Add(instance.GetType());
                }
                catch (Exception ex)
                {
                    Logger.Fatal($"AttachTickHandlers");
                    Logger.Fatal($"{ex}");
                }
            });
        }

        internal bool IsLoadingManager<T>() where T : Manager<T>, new()
        {
            return Managers.FirstOrDefault(self => self.Key == typeof(T)).Value is bool == false;
        }

        internal object GetManager(Type type)
        {
            return Managers.FirstOrDefault(self => self.Key == type).Value;
        }

        internal T GetManager<T>() where T : Manager<T>, new()
        {
            return (T)Managers.FirstOrDefault(self => self.Key == typeof(T)).Value;
        }

        /// <summary>
        /// Adds an event handler to the event handlers dictionary.
        /// </summary>
        /// <remarks>This event will not go through FxEvents</remarks>
        /// <param name="eventName"></param>
        /// <param name="delegate"></param>
        internal void AddEventHandler(string eventName, Delegate @delegate)
        {
            Logger.Debug($"Registered Event Handler '{eventName}'");
            EventHandlers[eventName] += @delegate;
        }

        /// <summary>
        /// Attaches a Tick
        /// </summary>
        /// <param name="task"></param>
        internal void AttachTickHandler(Func<Task> task)
        {
            Tick += task;
        }

        /// <summary>
        /// Detaches a Tick
        /// </summary>
        /// <param name="task"></param>
        internal void DetachTickHandler(Func<Task> task)
        {
            Tick -= task;
        }

        /// <summary>
        /// Gets the current game timer. This is the time in milliseconds since the game started.
        /// Using this as the only method to call GetGameTimer will lower the amount of calls to the native.
        /// </summary>
        /// <returns></returns>
        [TickHandler]
        private async Task OnUpdateGameTimerAsync()
        {
            GameTime = GetGameTimer();
            await BaseScript.Delay(500);
        }
    }
}