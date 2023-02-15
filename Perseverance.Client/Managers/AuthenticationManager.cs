namespace Perseverance.Client.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("authenticate", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                try
                {
                    body.TryGetValue("0", out object username);
                    body.TryGetValue("1", out object password);

                    Authenitcation authenitcation = new Authenitcation($"{username}", $"{password}");

                    EventMessage eventMessage = await EventDispatcher.Get<EventMessage>("server:authenticate", Game.Player.ServerId, authenitcation);

                    if (eventMessage == null)
                    {
                        Logger.Error($"[ConnectionManager] Failed to authenticate. Please try again or contact a server admin");
                        result(new EventMessage
                        {
                            errorMessage = "Failed to authenticate"
                        });
                        return;
                    }

                    result(eventMessage);
                }
                catch (Exception ex)
                {
                    result(new EventMessage
                    {
                        errorMessage = ex.Message
                    });
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
