namespace Perserverance.Shared.Models
{
    public partial class Authenitcation
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Authenitcation() { }

        internal Authenitcation(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
