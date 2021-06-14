namespace BattleCards.Services.Validation
{
    using BattleCards.Common;
    using BattleCards.Data;
    using BattleCards.ViewModels.Crads;
    using BattleCards.ViewModels.Users;
    using System.Linq;

    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ValidationService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        public bool IsValid { get; set; }

        public string Message { get; set; }


        public string ValidateCardData(InputCardViewModel inputCardView)
        {
            this.IsValid = true;

            if (inputCardView.Name.Length < ValidationConstants.CardNameMinLenght ||
                inputCardView.Name.Length > ValidationConstants.CardNameMaxLenght)
            {
                this.IsValid = false;
                this.Message = string.Format(ValidationConstants.InvalidTextLehghCardValues,
                    nameof(inputCardView.Name), ValidationConstants.CardNameMinLenght, ValidationConstants.CardNameMaxLenght);
            }

            if (string.IsNullOrWhiteSpace(inputCardView.Description) ||
                inputCardView.Description.Length > ValidationConstants.DescriptionMaxLenght)
            {
                this.IsValid = false;
                this.Message = string.Format(ValidationConstants.InvalidTextLehghCardValues,
                    nameof(inputCardView.Description), 1, ValidationConstants.DescriptionMaxLenght);
            }


            if (!CheckCardPowerPoints(inputCardView.Attack))
            {
                this.Message = string.Format(ValidationConstants.InvalidPowerPointsCardValues,
                    nameof(inputCardView.Attack));
            }

            if (!CheckCardPowerPoints(inputCardView.Health))
            {
                this.Message = string.Format(ValidationConstants.InvalidPowerPointsCardValues,
                    nameof(inputCardView.Health));
            }

            return this.Message;
        }

        public string ValidateUserData(InputUserViewModel inputUserView)
        {
            this.IsValid = true;

            var usernameLenght = inputUserView.Username.Length;
            if (!CheckUserValuesLenght(usernameLenght, ValidationConstants.UserNameMinLenght, ValidationConstants.UserNameMaxLenght))
            {
                this.IsValid = false;
                this.Message = 
                    string.Format(ValidationConstants.InvalidTextLehghUserValues,
                    nameof(inputUserView.Username), ValidationConstants.UserNameMinLenght, ValidationConstants.UserNameMaxLenght);
            }

            var passwordLenght = inputUserView.Password.Length;
            if (!CheckUserValuesLenght(passwordLenght, ValidationConstants.PasswordMinLenght, ValidationConstants.PasswordMaxLenght))
            {
                this.IsValid = false;
                this.Message = 
                    string.Format(ValidationConstants.InvalidTextLehghUserValues, 
                    nameof(inputUserView.Password), ValidationConstants.PasswordMinLenght, ValidationConstants.PasswordMaxLenght);
            }

            if (inputUserView.Password != inputUserView.ConfirmPassword)
            {
                this.IsValid = false;
                this.Message = ValidationConstants.InvalidPasssworld;
            }

            var users = this.applicationDbContext.Users.ToList();

            if (users.Select(x => x.Email).Contains(inputUserView.Email))
            {
                this.IsValid = false;
                this.Message = ValidationConstants.EmailUsed;
            }

            if (users.Select(x => x.Username).Contains(inputUserView.Username))
            {
                this.IsValid = false;
                this.Message = string.Format(ValidationConstants.UsernameUsed, inputUserView.Username);
            }

            return this.Message;
        }

        public string ValidateLoginData(InputLoginViewModel inputLoginView)
        {
            this.IsValid = true;

            if (string.IsNullOrEmpty(inputLoginView.Username) || string.IsNullOrEmpty(inputLoginView.Password))
            {
                this.IsValid = false;
                this.Message = ValidationConstants.InvalidLoginData;
            }

            var user = this.applicationDbContext.Users.ToList().FirstOrDefault(x => x.Username == inputLoginView.Username);

            if (user == null)
            {
                this.IsValid = false;
                this.Message = ValidationConstants.InvalidUsername;
            }

            if (user.Password != PasswordEncoder.EncodePassword(inputLoginView.Password) ||
                user.Username != inputLoginView.Username)
            {
                this.IsValid = false;
                this.Message = ValidationConstants.UnsuccessfulLogin;
            }

            return this.Message;
        }

        private bool CheckCardPowerPoints(int value)
        {
            if (value < 1)
            {
                this.IsValid = false;
                return false;
            }

            return true;
        }

        private bool CheckUserValuesLenght(int valueLenght, int minValueLenght, int maxValueLenght)
        {
            if (valueLenght < minValueLenght || valueLenght > maxValueLenght)
            {
                return false;
            }

            return true;
        }
    }
}
