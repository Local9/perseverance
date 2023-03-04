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
                    Dictionary<string, string> keyValuePairs = body.ToDictionary(x => x.Key, x => x.Value.ToString());

                    Authentication authentication = new Authentication(keyValuePairs["username"], keyValuePairs["password"]);

                    dynamic eventMessage = await EventDispatcher.Get<dynamic>("server:authenticate", Game.Player.ServerId, authentication);

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

            RegisterNuiCallback("register", new Action<IDictionary<string, object>, CallbackDelegate>(async (body, result) =>
            {
                try
                {
                    string registrationUrl = await EventDispatcher.Get<string>("server:getRegistrationUrl", Game.Player.ServerId);
                    result(registrationUrl);

                    //Dictionary<string, string> keyValuePairs = body.ToDictionary(x => x.Key, x => x.Value.ToString());

                    //Registration registration = new Registration()
                    //{
                    //    username = keyValuePairs["username"],
                    //    password = keyValuePairs["password"],
                    //    confirmPassword = keyValuePairs["confirmPassword"],
                    //    registrationCode = keyValuePairs["registrationCode"]
                    //};

                    //RegistrationMessage registrationMessage = await EventDispatcher.Get<RegistrationMessage>("server:register", Game.Player.ServerId, registration);

                    //if (registrationMessage == null)
                    //{
                    //    Logger.Error($"[ConnectionManager] Failed to register. Please try again or contact a server admin");
                    //    result(new EventMessage
                    //    {
                    //        errorMessage = "Failed to register"
                    //    });
                    //    return;
                    //}

                    //result(registrationMessage);
                }
                catch (Exception ex)
                {
                    result(new EventMessage
                    {
                        errorMessage = ex.Message
                    });
                }
            }));
        }
    }
}
