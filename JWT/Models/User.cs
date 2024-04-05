namespace JWT.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }

    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
