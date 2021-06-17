namespace BattleCards.Services.Cards
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        int AddCard(InputCardViewModel input);

        void AddCardToUserCollection(string userId, int cardId);

        IEnumerable<CardViewModel> AllCards();

        void DeleteCard(int cardId, string userId);

        IEnumerable<CardViewModel> GetUserCollection(string userId);
    }
}
