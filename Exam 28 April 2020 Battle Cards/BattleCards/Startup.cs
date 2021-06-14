namespace BattleCards
{
    using System.Collections.Generic;
    using BattleCards.Services.Cards;
    using BattleCards.Services.Users;
    using BattleCards.Services.Validation;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {

        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<ICardService, CardsService>();
            serviceCollection.Add<IValidationService, ValidationService>();
        }
    }
}
