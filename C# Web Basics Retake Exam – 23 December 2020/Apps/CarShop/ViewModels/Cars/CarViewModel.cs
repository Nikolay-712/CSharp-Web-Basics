namespace CarShop.ViewModels.Cars
{
    using CarShop.Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class CarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public string Image { get; set; }

        public IEnumerable<Issue> Issues { get; set; }

        public int RemainingIssues => this.Issues.Where(x => !x.IsFixed).Count();

        public int FixedIssues => this.Issues.Where(x => x.IsFixed).Count();

    }
}
