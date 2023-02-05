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
                            errorMessage = "Failed to authenticate"
                        };
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new EventMessage
                    {
                        errorMessage = ex.Message
                    };
                }
            }));

            NuiManager.AttachNuiHandler("register", new AsyncEventCallback(async metadata =>
            {
                try
                {
                    Registration registration = new Registration()
                    {
                        username = metadata.Find<string>(0),
                        password = metadata.Find<string>(1),
                        confirmPassword = metadata.Find<string>(2),
                        registrationCode = metadata.Find<string>(3),
                        captchaResult = metadata.Find<string>(4)
                    };

                    RegistrationMessage result = await EventDispatcher.Get<RegistrationMessage>("server:register", Game.Player.ServerId, registration);

                    if (result == null)
                    {
                        Logger.Error($"[ConnectionManager] Failed to register. Please try again or contact a server admin");
                        return new EventMessage
                        {
                            errorMessage = "Failed to register"
                        };
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new EventMessage
                    {
                        errorMessage = ex.Message
                    };
                }
            }));
        }
    }
}
