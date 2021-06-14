namespace BattleCards.Controllers
{
    using BattleCards.Common;
    using BattleCards.Services.Users;
    using BattleCards.Services.Validation;
    using BattleCards.ViewModels.Users;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IValidationService validationService;

        public bool UserAutetacation { get; private set; }

        public UsersController(IUserService userService, IValidationService validationService)
        {
            this.userService = userService;
            this.validationService = validationService;
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (UserAuthentication.IsLogged(this.User))
            {
                this.SignOut();
            }
            return this.Redirect("/");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(InputLoginViewModel inputLoginView)
        {
            this.validationService.ValidateLoginData(inputLoginView);

            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.SignIn(this.userService.Login(inputLoginView));
            
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(InputUserViewModel inputUserViewModel)
        {
            this.validationService.ValidateUserData(inputUserViewModel);

            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.userService.Register(inputUserViewModel);

            return this.Redirect("/Users/Login");
        }


        
    }
}
