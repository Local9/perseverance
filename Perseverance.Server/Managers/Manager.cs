using Logger;
using Perseverance.Server.Models;
using System.Collections.Concurrent;

namespace Perseverance.Server.Managers
{
    public abstract class Manager<T> where T : Manager<T>, new()
    {
        internal static T GetModule()
        {
            return Main.Instance.GetManager<T>() ?? (!Main.Instance.IsLoadingManager<T>()
                       ? (T)Main.Instance.LoadManager(typeof(T))
                       : null);
        }

        internal Main Instance => Main.Instance;
        internal PlayerList PlayerList => Main.PlayerList;
        internal ConcurrentDictionary<int, PerseveranceUser> ActiveSessions => Main.ActiveSessions;

        internal void Event(string name, Delegate @delegate) => Instance.AddEventHandler(name, @delegate);
        internal ExportDictionary Export => Main.ExportDictionary;
        internal Log Logger => Main.Logger;

        protected Manager()
        {
            
        }

        public virtual void Begin()
        {

        }
    }
}
