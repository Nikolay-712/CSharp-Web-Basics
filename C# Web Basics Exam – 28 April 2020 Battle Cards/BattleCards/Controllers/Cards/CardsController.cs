namespace BattleCards.Controllers.Cards
{
    using BattleCards.Services.Cards;
    using BattleCards.Services.Validation;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {
        private readonly IValidationService validationService;
        private readonly ICardsService cardsService;

        public CardsController(IValidationService validationService, ICardsService cardsService)
        {
            this.validationService = validationService;
            this.cardsService = cardsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(InputCardViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.validationService.ValidateCardData(input);
            if (!this.validationService.IsValid)
            {
                this.Error(this.validationService.Message);
            }

            var userId = this.User;
            var cardId = this.cardsService.AddCard(input);

            this.validationService.CheckCurrentCard(cardId, userId);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.cardsService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");//Redirect
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cards = this.cardsService.AllCards().ToList();

            return this.View(cards);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.User;
            this.cardsService.AddCardToUserCollection(userId, cardId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.User;
            var collection = this.cardsService.GetUserCollection(userId);

            return this.View(collection);
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.cardsService.DeleteCard(cardId, this.User);

            return this.Redirect("/Cards/Collection");
        }
    }
}
