namespace CarShop.Services.Cars
{
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        void AddCar(InputCarViewModel input, string userId);

        IEnumerable<CarViewModel> GetAllCars(string userId);
    }
}
