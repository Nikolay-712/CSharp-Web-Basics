namespace SharedTrip.ViewModels.Trips
{
    public class TripDetailsViewModel
    {
        //StartPoint EndPoint departureTime Seats Description

        public string TripId { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public string DepartureTime { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
