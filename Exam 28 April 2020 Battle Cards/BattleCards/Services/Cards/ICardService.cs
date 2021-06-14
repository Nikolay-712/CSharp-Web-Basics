namespace BattleCards.Services.Cards
{
    using BattleCards.ViewModels.Crads;
    using System.Collections.Generic;

    public interface ICardService
    {
        void AddCard(InputCardViewModel inputCardView);

        IEnumerable<CardViewModel> All();
    }
}
