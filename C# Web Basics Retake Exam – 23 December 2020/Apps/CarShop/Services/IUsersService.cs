using CarShop.ViewModels.Users;

namespace CarShop.Services
{
    public interface IUsersService
    {
        void Create(InputRegisterViewModel input);

        string Login(InputLoginViewModel input);
    }
}
