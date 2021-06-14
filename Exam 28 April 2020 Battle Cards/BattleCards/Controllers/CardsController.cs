namespace BattleCards.Controllers
{
    using BattleCards.Services.Validation;
    using BattleCards.Services.Cards;
    using BattleCards.ViewModels.Crads;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        private readonly ICardService cardService;
        private readonly IValidationService validationService;

        public CardsController(ICardService cardService, IValidationService validationService)
        {
            this.cardService = cardService;
            this.validationService = validationService;
        }

        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Collection()
        {
            return this.View();
        }

        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(InputCardViewModel inputCardView)
        {
            this.validationService.ValidateCardData(inputCardView);

            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.cardService.AddCard(inputCardView);

            return this.View();
        }
    }
}
