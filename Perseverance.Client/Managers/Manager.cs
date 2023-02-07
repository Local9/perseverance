using Logger;
using Perseverance.Client.GameInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perseverance.Client.Managers
{
    public abstract class Manager<T> where T : Manager<T>, new()
    {
        internal static T GetModule()
        {
            return Main.Instance.GetManager<T>() ?? (!Main.Instance.IsLoadingManager<T>()
                       ? (T)Main.Instance.LoadManager(typeof(T))
                       : null);
        }

        internal void Event(string name, Delegate @delegate) => Instance.AddEventHandler(name, @delegate);
        internal Main Instance => Main.Instance;
        internal Log Logger => Main.Logger;
        internal NuiManager NuiManager => Main.NuiManager;

        protected Manager()
        {

        }

        public virtual void Begin()
        {
            // Ignored
        }
    }
}
