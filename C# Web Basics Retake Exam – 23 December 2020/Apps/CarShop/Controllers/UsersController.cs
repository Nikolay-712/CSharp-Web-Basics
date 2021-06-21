namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.Services.Validation;
    using CarShop.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IValidationService validationService;
        private readonly IUsersService usersService;

        public UsersController(IValidationService validationService, IUsersService usersService)
        {
            this.validationService = validationService;
            this.usersService = usersService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(InputRegisterViewModel input)
        {
            this.validationService.ValidateRegisterData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.usersService.Create(input);

            return this.Redirect("/User/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(InputLoginViewModel input)
        {
            this.validationService.ValidateLoginData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            var userId = this.usersService.Login(input);
            this.SignIn(userId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.Logout();

            return this.Redirect("/");
        }
    }
}
