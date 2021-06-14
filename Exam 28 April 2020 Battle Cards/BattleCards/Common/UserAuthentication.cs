namespace BattleCards.Common
{
    public class UserAuthentication
    {
        public static bool IsLogged(string id)
        {
            if (id != null)
            {
                return true;
            }

            return false;
        }

    }
}
