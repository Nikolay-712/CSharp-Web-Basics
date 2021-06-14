namespace BattleCards.Services.Cards
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Crads;

    class CardsService : ICardService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CardsService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void AddCard(InputCardViewModel inputCardView)
        {
            var card = new Card(
                inputCardView.Name,
                inputCardView.Image,
                inputCardView.Keyword,
                inputCardView.Attack,
                inputCardView.Health,
                inputCardView.Description);

            this.applicationDbContext.Cards.Add(card);
            this.applicationDbContext.SaveChanges();

        }
    }
}
