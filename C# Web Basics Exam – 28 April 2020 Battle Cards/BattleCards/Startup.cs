namespace BattleCards
{
    using System.Collections.Generic;
    using BattleCards.Services.Cards;
    using BattleCards.Services.PasswordEncoding;
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
            serviceCollection.Add<IValidationService, ValidationService>();
            serviceCollection.Add<IPasswordEncoder, PasswordEncoder>();

            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ICardsService, CardsService>();
        }
    }
}
