namespace BattleCards.Services.Users
{
    using BattleCards.Common;
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Users;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Register(InputUserViewModel inputUserView)
        {
            var passwold = PasswordEncoder.EncodePassword(inputUserView.Password);
            var user = new User(inputUserView.Username, inputUserView.Email, passwold);

            this.applicationDbContext.Users.Add(user);
            this.applicationDbContext.SaveChanges();

        }

        public string Login(InputLoginViewModel inputLoginView)
        {
            var user = this.applicationDbContext.Users.ToList()
                .FirstOrDefault(x => x.Password == PasswordEncoder.EncodePassword(inputLoginView.Password) &&
                x.Username == inputLoginView.Username);

            return user.Id;
        }
    }
}
