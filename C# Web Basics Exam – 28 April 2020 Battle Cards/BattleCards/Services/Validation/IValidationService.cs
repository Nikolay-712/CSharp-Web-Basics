namespace BattleCards.Services.Validation
{
    using BattleCards.ViewModels.Cards;
    using BattleCards.ViewModels.Users;

    public interface IValidationService
    {
        bool IsValid { get; set; }

        string Message { get; set; }

        string ValidateRegisterData(InputRegisterViewModel input);

        string ValidateLoginData(InputLoginViewModel input);

        string ValidateCardData(InputCardViewModel input);

        string CheckCurrentCard(int cardId, string userId);


    }
}
