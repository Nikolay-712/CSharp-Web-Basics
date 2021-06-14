namespace BattleCards.Services.Validation
{
    using BattleCards.ViewModels.Crads;
    using BattleCards.ViewModels.Users;

    public interface IValidationService
    {
        bool IsValid { get; set; }

        string Message { get; set; }

        public string ValidateCardData(InputCardViewModel inputCardView);

        public string ValidateUserData(InputUserViewModel inputUserView);

        string ValidateLoginData(InputLoginViewModel inputLoginView);
    }
}
