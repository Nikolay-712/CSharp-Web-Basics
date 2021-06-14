namespace BattleCards.Services.Users
{
    using BattleCards.ViewModels.Users;

    public interface IUserService
    {
        void Register(InputUserViewModel inputUserView);

        void Login(InputLoginViewModel inputLoginView);
    }
}
