namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Services.Trips;
    using SharedTrip.Services.Validation;
    using SharedTrip.ViewModels.Trips;

    public class TripsController : Controller
    {
        private readonly IValidationService validationService;
        private readonly ITripService tripService;

        public TripsController(IValidationService validationService,ITripService tripService)
        {
            this.validationService = validationService;
            this.tripService = tripService;
        }

        public HttpResponse All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View("/Users/Login");
            }

           var trips =  this.tripService.All();

            if (trips == null)
            {
                return Redirect("/Trips/Add");
            }

            return this.View(trips);
        }
        public HttpResponse Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(InputTripViewModel input)
        {
            this.validationService.ValideateTripInputData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }


            if (!this.User.IsAuthenticated)
            {
                return this.View("/Users/Login");
            }

            this.tripService.AddTrip(input);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string TripId)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View("/Users/Login");
            }

            var trip = this.tripService.Details(TripId);

            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string TripId)
        {
           
            if (!this.User.IsAuthenticated)
            {
                return this.View("/Users/Login");
            }

            var userId = this.User.Id;
            
            if (!this.tripService.AddUserToTrip(userId, TripId))
            {
                return this.Redirect($"/Trips/Details?tripId={TripId}");
            }


            return this.Redirect("/Trips/All");
        }
    }
}
