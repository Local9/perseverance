﻿using CitizenFX.Core.Native;
using FxEvents.Shared;
using FxEvents.Shared.TypeExtensions;
using Perseverance.Client.GameInterface.Events;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace Perseverance.Client.GameInterface
{
    public class NuiManager
    {
        private bool _hasFocus;

        /// <summary>
        /// true if focus is active.
        /// </summary>
        public bool IsNuiFocusOn => _hasFocus;

        /// <summary>
        /// Returns cursor position with nui on
        /// </summary>
        public Point NuiCursorPosition
        {
            get
            {
                int x = 0, y = 0;
                GetNuiCursorPosition(ref x, ref y);

                return new Point(x, y);
            }
        }

        /// <summary>
        /// Enable/disable html game interface (NUI)
        /// </summary>
        /// <param name="hasFocus">to enable / disable focus</param>
        /// <param name="showCursor">to show or not the mouse cursor</param>
        public void SetFocus(bool hasFocus, bool showCursor = true)
        {
            SetNuiFocus(hasFocus, showCursor);
            _hasFocus = hasFocus;
        }

        /// <summary>
        /// Enable/disable html game interface (NUI) keeping the input for the game
        /// </summary>
        /// <param name="keepInput">if true input is for the game</param>
        public void SetFocusKeepInput(bool keepInput)
        {
            SetNuiFocusKeepInput(keepInput);
            if (!_hasFocus) _hasFocus = true;
        }

        /// <summary>
        /// sends a nui message
        /// </summary>
        /// <param name="data">object that will be serialized</param>
        public void SendMessage(object data)
        {
            SendNuiMessage(data.ToJson()); //use any json serialization you want
        }

        /// <summary>
        /// sends a nui message
        /// </summary>
        /// <param name="data">an already serialized object</param>
        public void SendMessage(string data)
        {
            SendNuiMessage(data);
        }

        /// <summary>
        /// Registers a Nui Callback with no data returned from nui
        /// </summary>
        /// <param name="@event">name of the nui event callback</param>
        /// <param name="action">the callback</param>
        public void RegisterCallback(string @event, Action action)
        {
            RegisterNuiCallbackType(@event);
            Main.Instance.AddEventHandler($"__cfx_nui:{@event}", new Action<IDictionary<string, object>, CallbackDelegate>((data, callback) =>
            {
                Main.Logger.Debug($"Called NUI Callback [{@event}] with Payload {data.ToJson()}");
                action();
                callback("ok");
            }));
        }

        /// <summary>
        /// Registers a Nui Callback with data returned from nui
        /// </summary>
        /// <param name="@event">name of the nui event callback</param>
        /// <param name="action">the callback</param>
        public void RegisterCallback<T>(string @event, Action<T> action)
        {
            RegisterNuiCallbackType(@event);
            Main.Instance.AddEventHandler($"__cfx_nui:{@event}", new Action<IDictionary<string, object>, CallbackDelegate>((data, callback) =>
            {
                Main.Logger.Debug($"Called NUI Callback {@event} with Payload {data.ToJson()} and type {typeof(T)}");
                T typedData = data.Count == 1 ? TypeCache<T>.IsSimpleType ? (T)data.Values.ElementAt(0) : data.Values.ElementAt(0).ToJson().FromJson<T>() : data.ToJson().FromJson<T>();
                action(typedData);
                callback("ok");
            }));
        }

        /// <summary>
        /// Registers a Nui Callback and sends back to nui the result of the callback
        /// </summary>
        /// <param name="@event">name of the nui event callback</param>
        /// <param name="action">the callback</param>
        public void RegisterCallback<TReturn>(string @event, Func<TReturn> action)
        {
            RegisterNuiCallbackType(@event);
            Main.Instance.AddEventHandler($"__cfx_nui:{@event}", new Action<IDictionary<string, object>, CallbackDelegate>((data, callback) =>
            {
                Main.Logger.Debug($"Called NUI Callback {@event} with Payload {data.ToJson()}");
                TReturn result = action();
                callback(result.ToJson());
            }));
        }

        /// <summary>
        /// Registers an async Nui Callback and sends back to nui the result of the callback
        /// </summary>
        /// <param name="@event">name of the nui event callback</param>
        /// <param name="action">the callback</param>
        public void RegisterCallback<T, TReturn>(string @event, Func<T, TReturn> action)
        {
            RegisterNuiCallbackType(@event);
            Main.Instance.AddEventHandler($"__cfx_nui:{@event}", new Action<IDictionary<string, object>, CallbackDelegate>((data, callback) =>
            {
                Main.Logger.Debug($"Called NUI Callback {@event} with Payload {data.ToJson()}");
                T typedData = data.Count == 1 ? TypeCache<T>.IsSimpleType ? (T)data.Values.ElementAt(0) : data.Values.ElementAt(0).ToJson().FromJson<T>() : data.ToJson().FromJson<T>();
                TReturn result = action(typedData);
                callback(result.ToJson());
            }));
        }

        public void AttachNuiHandler(string pipe, EventCallback callback)
        {
            // Logger.Info($"[NUI]: {pipe}");
            API.RegisterNuiCallbackType(pipe);

            Main.Instance.AddEventHandler($"__cfx_nui:{pipe}", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                var metadata = new Events.EventMetadata();
                
                if (body != null)
                {
                    foreach (var entry in body)
                    {
                        if (!int.TryParse(entry.Key, out var index))
                        {
                            Main.Logger.Info($"[Nui] [{pipe}] Payload `{entry.Key}` is not a number and therefore not written to.");

                            continue;
                        }

                        if (entry.Value is null)
                        {
                            Main.Logger.Error($"[Nui] [{pipe}]  Payload `{entry.Key}` value is null. Ending message.");
                            result(new { success = false });
                            return;
                        }

                        metadata.Write(index, entry.Value);
                    }
                }

                if (callback.GetType() == typeof(AsyncEventCallback))
                {
                    result(await ((AsyncEventCallback)callback).AsyncTask(metadata));
                }
                else
                {
                    result(callback.Task(metadata));
                }
            }));
        }
    }
}
