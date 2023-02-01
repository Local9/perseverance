namespace Perserverance.Client.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            NuiManager.AttachNuiHandler("authenticate", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    Authenitcation authenitcation = new Authenitcation(metadata.Find<string>(0), metadata.Find<string>(1));
                    EventMessage result = await EventDispatcher.Get<EventMessage>("server:authenticate", Game.Player.ServerId, authenitcation);

                    if (result == null)
                    {
                        Logger.Error($"[ConnectionManager] Failed to authenticate. Please try again or contact a server admin");
                        return new EventMessage
                        {
                            success = false,
                            message = "Failed to authenticate"
                        };
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new EventMessage
                    {
                        success = false,
                        message = ex.Message
                    };
                }
            }));
        }
    }
}
