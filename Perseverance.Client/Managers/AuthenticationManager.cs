namespace Perseverance.Client.Managers
{
    public class AuthenticationManager : Manager<AuthenticationManager>
    {
        public override void Begin()
        {
            RegisterNuiCallback("authenticate", new Action<IDictionary<string, object>, CallbackDelegate>(OnAuthenticateAsync));
            RegisterNuiCallback("register", new Action<IDictionary<string, object>, CallbackDelegate>(OnGetRegistrationUrlAsync));
        }

        private async void OnAuthenticateAsync(IDictionary<string, object> body, CallbackDelegate result)
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
        }

        private async void OnGetRegistrationUrlAsync(IDictionary<string, object> body, CallbackDelegate result)
        {
            try
            {
                string registrationUrl = await EventDispatcher.Get<string>("server:getRegistrationUrl", Game.Player.ServerId);
                result(registrationUrl);
            }
            catch (Exception ex)
            {
                result(new EventMessage
                {
                    errorMessage = ex.Message
                });
            }
        }
    }
}
