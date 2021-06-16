namespace Git.Services.Users
{
    using Git.ViewModels.Users;

    public interface IUsersService
    {
        void CreateUser(InputRegisterViewModel input);

        string Login(InputLoginViewModel input);

    }
}
