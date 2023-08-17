using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.UI.Desktop.ViewModels
{
    public class CarsViewModel
    {
        private readonly IRepository repo;

        public CarsViewModel(IRepository repo)
        {
            this.repo = repo;
            CarList = new List<Car>(repo.GetAll<Car>());
        }

        //todo KILL IT
        public CarsViewModel() : this(new Data.EfCore.CarRentalXPressContextRepositoryAdapter("Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true"))
        { }

        public List<Car> CarList { get; set; }

        public Car SelectedCar { get; set; }
    }
}
