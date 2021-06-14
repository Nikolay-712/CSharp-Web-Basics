namespace BattleCards.Services.Users
{
    using BattleCards.Common;
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Users;
    

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

        public void Login(InputLoginViewModel inputLoginView)
        {

        }

       
    }
}
