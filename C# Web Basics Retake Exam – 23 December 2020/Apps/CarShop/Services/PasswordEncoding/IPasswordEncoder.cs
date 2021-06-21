namespace CarShop.Services.PasswordEncoding
{
    public interface IPasswordEncoder
    {
        string ComputeHash(string input);
    }
}
