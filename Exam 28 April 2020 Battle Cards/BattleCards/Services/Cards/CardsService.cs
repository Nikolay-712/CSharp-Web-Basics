namespace BattleCards.Services.Cards
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Crads;
    using System.Linq;
    using System.Collections.Generic;

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

        public IEnumerable<CardViewModel> All()
        {
            var cards = this.applicationDbContext
                .Cards
                .ToList()
                .Select(x => 
                new CardViewModel 
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Keyword = x.Keyword,
                    Attack = x.Attack,
                    Health = x.Health,
                    Description = x.Description,
                }).ToList();

            return cards;
        }

        public IEnumerable<CardViewModel> UserCollection(string id)
        {
            return null;
        }
    }
}

