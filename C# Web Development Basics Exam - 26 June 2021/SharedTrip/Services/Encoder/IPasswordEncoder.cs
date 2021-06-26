namespace SharedTrip.Services.Encoder
{
   public interface IPasswordEncoder
    {
        string ComputeHash(string input);
    }
}
