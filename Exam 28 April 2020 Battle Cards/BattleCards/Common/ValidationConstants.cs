namespace BattleCards.Common
{
    public class ValidationConstants
    {
        public const int CardNameMinLenght = 5;
        public const int CardNameMaxLenght = 15;
        public const int DescriptionMaxLenght = 200;
        public static string InvalidTextLehghCardValues = "The card {0} must be between {1} and {2} characters!";
        public static string InvalidPowerPointsCardValues = "The card {0} must be greater than ( 0 ) points!";
        public static string CardnameUsed = "This name has already been added!";

        public const int UserNameMinLenght = 5;
        public const int UserNameMaxLenght = 20;
        public const int PasswordMinLenght = 6;
        public const int PasswordMaxLenght = 20;
        public static string InvalidTextLehghUserValues = "{0} must be between {1}  and {2} characters!";
        public static string InvalidPasssworld = "Password does not match !";
        public static string EmailUsed = "This email is already in use!";
        public static string UsernameUsed = "This username {0} is already in use!";
        public static string InvalidLoginData = "Username and Password are required!";
        public static string InvalidUsername = "There is no such username!";
        public static string UnsuccessfulLogin = "Username or password do not match!";
    }
}
