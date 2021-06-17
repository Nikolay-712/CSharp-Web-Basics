using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services.PasswordEncoding;
using BattleCards.ViewModels.Users;
using System.Linq;

namespace BattleCards.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPasswordEncoder passwordEncoder;

        public UsersService(ApplicationDbContext applicationDbContext,IPasswordEncoder passwordEncoder)
        {
            this.applicationDbContext = applicationDbContext;
            this.passwordEncoder = passwordEncoder;
        }

        public string Login(InputLoginViewModel input)
        {
            var user = this.applicationDbContext.Users.ToList().FirstOrDefault(x => x.Username == input.Username);
            return user.Id;
        }

        public void Register(InputRegisterViewModel input)
        {
            var user = new User
            {
                Email = input.Email,
                Username = input.Username,
                Password = this.passwordEncoder
                .ComputeHash(input.Password),
            };

            this.applicationDbContext.Users.Add(user);
            this.applicationDbContext.SaveChanges();
        }

    }
}
