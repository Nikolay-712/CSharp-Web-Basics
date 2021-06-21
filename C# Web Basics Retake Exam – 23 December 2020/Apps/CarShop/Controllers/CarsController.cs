namespace CarShop.Controllers
{
    using CarShop.Services.Cars;
    using CarShop.Services.Validation;
    using CarShop.ViewModels.Cars;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IValidationService validationService;

        public CarsController(ICarService carService, IValidationService validationService)
        {
            this.carService = carService;
            this.validationService = validationService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            var cars = this.carService.GetAllCars(userId);

            return this.View(cars);
        }


        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(InputCarViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            
            this.validationService.ValdateCarData(input, this.GetUserId());
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.carService.AddCar(input, this.GetUserId());
            return this.Redirect("/Cars/All");
        }
    }
}
