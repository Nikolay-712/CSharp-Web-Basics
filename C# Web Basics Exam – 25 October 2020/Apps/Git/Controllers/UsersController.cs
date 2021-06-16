namespace Git.Controllers
{
    using Git.Services.Users;
    using Git.Services.Validation;
    using Git.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;

    class UsersController : Controller
    {
        private readonly IModelValidation modelValidation;
        private readonly IUsersService usersService;

        public UsersController(IModelValidation modelValidation, IUsersService usersService)
        {
            this.modelValidation = modelValidation;
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }

        [HttpPost]
        public HttpResponse Login(InputLoginViewModel input)
        {
            this.modelValidation.ValidateLoginData(input);
            if (!this.modelValidation.IsValid)
            {
                return this.Error(this.modelValidation.Message);
            }

            this.SignIn(this.usersService.Login(input));

            return this.Redirect("/Repositories/All");

        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(InputRegisterViewModel input)
        {
            this.modelValidation.ValidateRegisterData(input);

            if (!this.modelValidation.IsValid)
            {
                return this.Error(this.modelValidation.Message);
            }

            this.usersService.CreateUser(input);

            return this.Redirect("/Users/Login");
        }
    }
}
