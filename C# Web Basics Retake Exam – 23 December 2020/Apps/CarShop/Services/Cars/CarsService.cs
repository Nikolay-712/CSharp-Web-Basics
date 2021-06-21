namespace CarShop.Services.Cars
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsService : ICarService
    {
        private readonly ApplicationDbContext dbContext;

        public CarsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddCar(InputCarViewModel input, string userId)
        {
            var car = new Car
            {
                Model = input.Model,
                Year = input.Year,
                PictureUrl = input.Image,
                PlateNumber = input.PlateNumber,
                OwnerId = userId,
            };

            this.dbContext.Cars.Add(car);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<CarViewModel> GetAllCars(string userId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (user.IsMechanic)
            {
                var allCars = this.dbContext.Cars
                    .Where(x => x.Issues
                    .Any(x => x.IsFixed == false))
                    .Select(x => new CarViewModel
                    {
                        Id = x.Id,
                        Model = x.Model,
                        PlateNumber = x.PlateNumber,
                        Image = x.PictureUrl,
                        Issues = x.Issues.Where(ci => ci.CarId == x.Id)
                    }
                ).ToList();

                return allCars;
            }



            var cars = this.dbContext
                .Cars.Where(x => x.OwnerId == userId)
                .Select(x => new CarViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    PlateNumber = x.PlateNumber,
                    Image = x.PictureUrl,
                    Issues = x.Issues.Where(ci => ci.CarId == x.Id && x.OwnerId == userId)
                }
                ).ToList();


            return cars;
        }
    }
}
