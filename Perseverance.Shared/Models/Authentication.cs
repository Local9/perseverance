namespace Perseverance.Shared.Models
{
    public partial class Authentication
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Authentication() { }

        internal Authentication(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
