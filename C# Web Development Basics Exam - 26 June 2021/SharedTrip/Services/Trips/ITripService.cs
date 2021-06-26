using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services.Trips
{
    public interface ITripService
    {
        void AddTrip(InputTripViewModel input);

        TripDetailsViewModel Details(string id);

        IEnumerable<TripDetailsViewModel> All();

        bool AddUserToTrip(string userId, string tripId);

    }
}
