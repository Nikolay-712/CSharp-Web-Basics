namespace BattleCards.Services.Cards
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext applcationDbContext;

        public CardsService(ApplicationDbContext applcationDbContext)
        {
            this.applcationDbContext = applcationDbContext;
        }

        public int AddCard(InputCardViewModel input)
        {
            var card = new Card
            {
                Name = input.Name,
                ImageUrl = input.Image,
                Keyword = input.Keyword,
                Attack = input.Attack,
                Health = input.Health,
                Description = input.Description,
            };

            this.applcationDbContext.Cards.Add(card);
            this.applcationDbContext.SaveChanges();

            return card.Id;
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            var userCards = this.applcationDbContext.UserCards.ToList().Where(x => x.UserId == userId);

            if (userCards.Any(x => x.CardId == cardId))
            {
                return;
            }

            var userCard = new UserCard
            {
                CardId = cardId,
                UserId = userId,
            };

            this.applcationDbContext.UserCards.Add(userCard);
            this.applcationDbContext.SaveChanges();
        }

        public IEnumerable<CardViewModel> AllCards()
        {
            var cards = this.applcationDbContext
                .Cards.ToList()
                .Select(x => new CardViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.ImageUrl,
                    Keyword = x.Keyword,
                    Description = x.Description,
                    Attack = x.Attack,
                    Health = x.Health,

                });

            return cards;
        }

        public void DeleteCard(int cardId, string userId)
        {
            var card = this.applcationDbContext.UserCards.ToList().Where(x => x.UserId == userId).FirstOrDefault(x => x.CardId == cardId);

            this.applcationDbContext.UserCards.Remove(card);
            this.applcationDbContext.SaveChanges();
        }

        public IEnumerable<CardViewModel> GetUserCollection(string userId)
        {
            var cards = this.applcationDbContext.UserCards
                .Where(x => x.UserId == userId)
                .Select(x => new CardViewModel
                {
                    Id = x.CardId,
                    Name = x.Card.Name,
                    Image = x.Card.ImageUrl,
                    Description = x.Card.Description,
                    Keyword = x.Card.Keyword,
                    Health = x.Card.Health,
                    Attack = x.Card.Attack,

                }).ToList();

            return cards;
        }
    }
}
