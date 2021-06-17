namespace BattleCards.Services.PasswordEncoding
{
    public interface IPasswordEncoder
    {
        string ComputeHash(string input);
    }
}
