using FxEvents.Shared;

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
    }
}
