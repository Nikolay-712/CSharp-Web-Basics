namespace CarShop.Data.Models
{
    using System;

    public class Issue
    {
        public Issue()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public bool IsFixed { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
