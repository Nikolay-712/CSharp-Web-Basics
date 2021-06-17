namespace BattleCards.Controllers.Users
{
    using SIS.HTTP;
    using SIS.MvcFramework;
    using BattleCards.Services.Users;
    using BattleCards.ViewModels.Users;
    using BattleCards.Services.Validation;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IValidationService validationService;

        public UsersController(IUsersService usersService, IValidationService validationService)
        {
            this.usersService = usersService;
            this.validationService = validationService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(InputLoginViewModel input)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            this.validationService.ValidateLoginData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            var userId = this.usersService.Login(input);
            this.SignIn(userId);

            return this.Redirect("/Cards/All");

        }

        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(InputRegisterViewModel input)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            this.validationService.ValidateRegisterData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(validationService.Message);
            }

            this.usersService.Register(input);

            return this.Redirect("/Users/Login"); //Redirect
        }

    }
}
