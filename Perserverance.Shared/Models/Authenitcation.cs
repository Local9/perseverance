namespace Perserverance.Shared.Models
{
    public partial class CadAuthenitcation
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public CadAuthenitcation() { }

        internal CadAuthenitcation(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
