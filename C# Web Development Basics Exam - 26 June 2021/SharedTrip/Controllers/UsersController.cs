using MyWebServer.Controllers;
namespace SharedTrip.Controllers
{
    using SharedTrip.Services.Users;
    using SharedTrip.Services.Validation;
    using SharedTrip.ViewModels;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IValidationService validationService;
        private readonly IUserService userService;

        public UsersController(IValidationService validationService, IUserService userService)
        {
            this.validationService = validationService;
            this.userService = userService;
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Trips/All");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(InputRegisterViewModel input)
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Trips/All");
            }

            this.validationService.ValidateRegisterData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.userService.Register(input);


            return this.Redirect("/Users/Login");
        }



        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Trips/All");
            }

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

            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Trips/All");
            }

            var userId = this.userService.Login(input);
            this.SignIn(userId);


            return this.Redirect("/Trips/All");
        }


        public HttpResponse Logout()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
