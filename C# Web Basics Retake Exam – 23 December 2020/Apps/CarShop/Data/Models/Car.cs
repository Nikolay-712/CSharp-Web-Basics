namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Issues = new HashSet<Issue>();
        }
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string PictureUrl { get; set; }

        public string PlateNumber { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Issue> Issues{ get; set; }
    }
}
