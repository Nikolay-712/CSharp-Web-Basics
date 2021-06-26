namespace SharedTrip.Services.Users
{
    using SharedTrip.ViewModels;

    public interface IUserService
    {
        public void Register(InputRegisterViewModel input);

        public string Login(InputLoginViewModel input);
    }
}
