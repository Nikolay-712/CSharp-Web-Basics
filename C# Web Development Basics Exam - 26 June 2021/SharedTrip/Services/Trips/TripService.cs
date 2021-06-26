namespace SharedTrip.Services.Trips
{
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Services.Validation;
    using SharedTrip.ViewModels.Trips;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class TripService : ITripService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IValidationService validationService;

        public TripService(ApplicationDbContext applicationDbContext, IValidationService validationService)
        {
            this.applicationDbContext = applicationDbContext;
            this.validationService = validationService;
        }

        public void AddTrip(InputTripViewModel input)
        {
            this.validationService.ValideateTripInputData(input);

            var departureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = departureTime,
                ImagePath = input.ImagePath,
                Seats = input.Seats,
                Description = input.Description,

            };

            this.applicationDbContext.Trips.Add(trip);
            this.applicationDbContext.SaveChanges();

        }

        public TripDetailsViewModel Details(string id)
        {
            var trip = this.applicationDbContext.Trips.Select(x => new
            TripDetailsViewModel
            {
                TripId = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                Seats = x.Seats - x.UserTrips.Count(),
                Description = x.Description,
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
            }
            ).FirstOrDefault(x => x.TripId == id);


            return trip;

        }

        public IEnumerable<TripDetailsViewModel> All()
        {
            var trips = this.applicationDbContext.Trips.Select(x =>
                new TripDetailsViewModel
                {
                    TripId = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH.mm:ss"),
                    Seats = x.Seats - x.UserTrips.Count(),

                }).ToList();

            return trips;
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var userInTrip = this.applicationDbContext.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);
            var trip = this.applicationDbContext.Trips.FirstOrDefault(x => x.Id == tripId);

            if (userInTrip)
            {
                return false;
            }

            if (trip.Seats == 0)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId,
            };

            this.applicationDbContext.UserTrips.Add(userTrip);
            this.applicationDbContext.SaveChanges();

            return true;

        }
    }
}
