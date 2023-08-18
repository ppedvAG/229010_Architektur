using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.Core
{
    public class RentServices : IRentServices
    {
        private readonly IUnitOfWork unitOfWork;

        public RentServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetAvailableCars(DateTime day, string location)
        {
            // Get all cars from the repository
            IEnumerable<Car> allCars = unitOfWork.CarRepository.GetAll();

            // Get rented cars for the specified day and location from the repository
            IEnumerable<Car> rentedCarsForDayAndLocation = unitOfWork
                .RentRepository.Query()
                .Where(rent =>
                    rent.StartDate.Date <= day.Date && rent.EndDate.Date >= day.Date &&
                    rent.StartLocation == location)
                .Select(rent => rent.Car);

            // Return the difference between all cars and rented cars for the specified day and location
            IEnumerable<Car> availableCars = allCars.Except(rentedCarsForDayAndLocation);

            return availableCars;
        }


        public void StartRent(Rent rent, string location)
        {
            //todo Validate Customer
            rent.StartDate = DateTime.Now;
            rent.StartLocation = location;
            //..
        }

        public void EndRent(Rent rent, string location)
        {
            rent.EndDate = DateTime.Now;
            rent.EndLocation = location;
            //..
        }
    }
}
