namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Services.PasswordEncoding;
    using CarShop.ViewModels.Users;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordEncoder passwordEncoder;

        public UsersService(ApplicationDbContext dbContext, IPasswordEncoder passwordEncoder)
        {
            this.dbContext = dbContext;
            this.passwordEncoder = passwordEncoder;
        }


        public void Create(InputRegisterViewModel input)
        {
            var user = new User
            {
                Username = input.Username,
                Email = input.Email,
                Password = this.passwordEncoder.ComputeHash(input.Password),
            };

            user.IsMechanic = true;
            if (input.UserType.ToLower() == "client")
            {
                user.IsMechanic = false;
            }

            this.dbContext.Add(user);
            this.dbContext.SaveChanges();
        }

        public string Login(InputLoginViewModel input)
        {
            var user = this.dbContext
                .Users.Where(x => x.Username == input.Username && x.Password == this.passwordEncoder.ComputeHash(input.Password))
                .FirstOrDefault();

            return user.Id;
        }

    }
}
