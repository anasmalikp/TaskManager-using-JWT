using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace JWT.Models
{
    public interface IUser
    {
        public void register(User user);
        public User login(Login login);
        public List<User> getuser();
    }

    public class services : IUser
    {
        public List<User> users = new List<User>
        {
            new User {Id = 1, Username = "anasmalik", Password="1234", role = "admin"},
            new User {Id = 2, Username = "zaynmalik", Password = "4321", role = "user"}
        };
        public void register(User user)
        {
            users.Add(user);
        }

        public User login(Login login)
        {
            var person = users.FirstOrDefault(x => x.Username == login.username && x.Password == login.password);
            return person;
        }

        public List<User> getuser()
        {
            return users;
        }
    }
}
