using FxEvents;
using Logger;
using Perserverance.Server.Managers;
using Perserverance.Server.Models;
using Perserverance.Server.SnailyCAD;
using Perserverance.Shared.Attributes;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;

namespace Perserverance.Server
{
    public class Main : BaseScript
    {
        internal static Main Instance { get; private set; }
        internal static Log Logger { get; private set; }
        internal static PlayerList PlayerList { get; private set; }
        internal static ExportDictionary ExportDictionary { get; private set; }
        internal static Random Random = new Random(DateTime.UtcNow.Millisecond);
        internal static ServerConfiguration ServerConfiguration { get; private set; }
        internal static string SnailyCadUrl { get; private set; }
        internal static string SnailyCadApiKey { get; private set; }

        internal static long GameTime = GetGameTimer();
        internal static ConcurrentDictionary<int, PerserveranceUser> ActiveSessions = new();
        public Dictionary<Type, List<MethodInfo>> TickHandlers { get; set; } = new();
        public List<Type> RegisteredTickHandlers { get; set; } = new();
        public Dictionary<Type, object> Managers { get; } = new();
        public static bool IsOneSyncEnabled => GetConvar("onesync", "off") != "off";
        public static bool IsServerReady;

        public Main()
        {
            Instance = this;
            Logger = new();
            PlayerList = Players;
            ExportDictionary = Exports;

            Load();
        }

        private async void Load()
        {
            ServerConfiguration = new ServerConfiguration();
            
            // Currently no use for a database but no need to delete the code as there are reasons to add one in the future.
            
            //bool databaseTest = await Database.DapperDatabase<bool>.GetSingleAsync("select 1;");
            //if (databaseTest)
            //{
            //    Logger.Info($"Database Connection Test Successful!");
            //}
            //else
            //{
            //    Logger.Error($"Database Connection Test Failed!");
            //}

            SnailyCadUrl = GetConvar("snailycad_url", "unknown");
            SnailyCadApiKey = GetConvar("snailycad_api_key", "unknown");

            if (SnailyCadUrl == "unknown" || SnailyCadApiKey == "unknown")
            {
                Logger.Error($"SnailyCAD is not configured correctly. Please check your server.cfg.");
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                
                HttpResponseMessage httpResponseMessage = await HttpHandler.OnHttpResponseMessageAsync(HttpMethod.Get, SnailyCadUrl);
                if (httpResponseMessage is not null)
                {
                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                        Logger.Info($"HTTP Connection Test to SnailyCAD was successful. URL: {SnailyCadUrl}");
                    else
                        Logger.Error($"HTTP Connection Test Failed! Returned Status Code: {httpResponseMessage.StatusCode}");
                }
                else
                {
                    Logger.Error($"HTTP Connection Test Failed! Unable to contact Snaily CAD API.");
                }
            }

            LoadManagers();
        }

        void LoadManagers()
        {
            Assembly.GetExecutingAssembly().GetExportedTypes()
                .SelectMany(self => self.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                .Where(self => self.GetCustomAttribute(typeof(TickHandler), false) != null).ToList()
                .ForEach(self =>
                {
                    var type = self.DeclaringType;

                    if (type == null) return;

                    if (!TickHandlers.ContainsKey(type))
                    {
                        TickHandlers.Add(type, new List<MethodInfo>());
                    }

                    Logger.Debug($"[TickHandlers] {type.Name}::{self.Name}");

                    TickHandlers[type].Add(self);
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

            Logger.Info($"Loaded {loaded} managers");

            IsServerReady = !IsServerReady;
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

        internal bool IsLoadingManager<T>() where T : Manager<T>, new()
        {
            return !(Managers.FirstOrDefault(self => self.Key == typeof(T)).Value is bool);
        }

        internal object GetManager(Type type)
        {
            return Managers.FirstOrDefault(self => self.Key == type).Value;
        }

        internal T GetManager<T>() where T : Manager<T>, new()
        {
            return (T)Managers.FirstOrDefault(self => self.Key == typeof(T)).Value;
        }

        internal void AttachTickHandlers(object instance)
        {
            TickHandlers.TryGetValue(instance.GetType(), out var methods);

            methods?.ForEach(self =>
            {
                var handler = (TickHandler)self.GetCustomAttribute(typeof(TickHandler));

                Tick += (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), instance, self);

                RegisteredTickHandlers.Add(instance.GetType());
            });
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
        /// awaitable tick handler for async methods to use to check if the server is ready.
        /// </summary>
        /// <returns></returns>
        internal static async Task IsReadyAsync()
        {
            while (!IsServerReady)
            {
                await BaseScript.Delay(100);
            }
        }

        internal static void SendAll(string eventName, params object[] args)
        {
            EventDispatcher.Send(PlayerList, eventName, args);
        }

        [TickHandler]
        private async Task OnUpdateGameTimerAsync()
        {
            GameTime = GetGameTimer();
            await Task.FromResult(0);
        }

        public static PerserveranceUser ToPerserveranceUser(string handle)
        {
            return ToPerserveranceUser(int.Parse(handle));
        }

        public static PerserveranceUser ToPerserveranceUser(int handle)
        {
            return ActiveSessions.TryGetValue(handle, out PerserveranceUser user) ? user : null;
        }
    }
}