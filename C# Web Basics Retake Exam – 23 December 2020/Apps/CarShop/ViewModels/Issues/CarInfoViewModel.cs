namespace CarShop.ViewModels.Issues
{
    using CarShop.Data.Models;
    using System.Collections.Generic;
    public class CarInfoViewModel
    {
        public string CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public IEnumerable<Issue> Issues { get; set; }

    }
}
