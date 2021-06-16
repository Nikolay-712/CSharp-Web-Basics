namespace Git.Services.Users
{
    using Git.Data;
    using Git.Models;
    using Git.PasswordEncoding;
    using Git.ViewModels.Users;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UsersService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void CreateUser(InputRegisterViewModel input)
        {
            var user = new User
            {
                Username = input.Username,
                Email = input.Email,
                Password = EncodingPassword.ComputeHash(input.Password)
            };

            this.applicationDbContext.Users.Add(user);
            this.applicationDbContext.SaveChanges();
        }

        public string Login(InputLoginViewModel input)
        {
            var userId = this.applicationDbContext
                .Users.ToList()
                .FirstOrDefault(x => x.Username == input.Username &&
                x.Password == EncodingPassword.ComputeHash(input.Password)).Id;

            return userId;
        }

      
    }
}
