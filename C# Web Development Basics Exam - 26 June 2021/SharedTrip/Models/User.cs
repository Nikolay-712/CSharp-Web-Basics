using System;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserTrips = new HashSet<UserTrip>();
        }


        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; }
    }
}
