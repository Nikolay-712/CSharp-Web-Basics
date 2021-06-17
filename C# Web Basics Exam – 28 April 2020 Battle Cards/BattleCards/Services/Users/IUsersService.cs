using BattleCards.ViewModels.Users;

namespace BattleCards.Services.Users
{
    public interface IUsersService
    {
        string Login(InputLoginViewModel input);

        void Register(InputRegisterViewModel input);
    }
}
