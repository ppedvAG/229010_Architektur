using NSubstitute;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.Core.Tests
{
    public class RentServicesTests
    {
        [Fact]
        public void GetAvailableCars_3_of_3_Cars_is_available()
        {
            var car1 = new Car { Manufacturer = "Toyota", Model = "Corolla", Color = "Blue", KW = 150 };
            var car2 = new Car { Manufacturer = "Honda", Model = "Civic", Color = "Red", KW = 120 };
            var car3 = new Car { Manufacturer = "Ford", Model = "Focus", Color = "Green", KW = 130 };
            var repo = Substitute.For<IRepository<Car>>();
            repo.GetAll().Returns(new[] { car1, car2, car3 });

            var uow = Substitute.For<IUnitOfWork>();
            uow.CarRepository.Returns(repo);
            var rentService = new RentServices(uow);

            var result = rentService.GetAvailableCars(DateTime.Now, "Heidelberg");

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAvailableCars_2_of_3_Cars_is_available()
        {
            var car1 = new Car { Manufacturer = "Toyota", Model = "Corolla", Color = "Blue", KW = 150 };
            var car2 = new Car { Manufacturer = "Honda", Model = "Civic", Color = "Red", KW = 120 };
            var car3 = new Car { Manufacturer = "Ford", Model = "Focus", Color = "Green", KW = 130 };
            var cust = new Customer() { Name = "Fred" };
            var rent1 = new Rent()
            {
                Car = car1,
                Customer = cust,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                StartLocation = "Heidelberg"
            };
            var carRepo = Substitute.For<IRepository<Car>>();
            var rentRepo = Substitute.For<IRepository<Rent>>();
      
            carRepo.GetAll().Returns(new[] { car1, car2, car3 });
            rentRepo.GetAll().Returns(new[] { rent1 });
            var uow = Substitute.For<IUnitOfWork>();
            uow.CarRepository.Returns(carRepo);
            uow.RentRepository.Returns(rentRepo);
            
            var rentService = new RentServices(uow);

            var result = rentService.GetAvailableCars(DateTime.Now, "Heidelberg");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAvailableCars_ReturnsAvailableCars()
        {
            // Arrange
            var carRepository = Substitute.For<IRepository<Car>>();
            var rentRepository = Substitute.For<IRepository<Rent>>();
            var uow = Substitute.For<IUnitOfWork>();
            uow.CarRepository.Returns(carRepository);
            uow.RentRepository.Returns(rentRepository);
            

            var rentServices = new RentServices(uow);

            var day = new DateTime(2023, 8, 17);
            var location = "LocationA";

            var car1 = new Car { Manufacturer = "Toyota", Model = "Corolla", Color = "Blue", KW = 150 };
            var car2 = new Car { Manufacturer = "Honda", Model = "Civic", Color = "Red", KW = 120 };
            var car3 = new Car { Manufacturer = "Ford", Model = "Focus", Color = "Green", KW = 130 };
            var cust = new Customer() { Name = "Fred" };

            var rentedCar = new Car { Manufacturer = "Chevrolet", Model = "Impala", Color = "Black", KW = 200 };

            carRepository.GetAll().Returns(new List<Car> { car1, car2, car3 });

            // Rented car overlaps with the specified day and location
            rentRepository.GetAll().Returns(new List<Rent>
            {
                new Rent {Customer=cust, Car = rentedCar, StartDate = day.AddDays(-1), EndDate = day.AddDays(1), StartLocation = location }
            });

            // Act
            var availableCars = rentServices.GetAvailableCars(day, location);

            // Assert
            Assert.Contains(car1, availableCars);
            Assert.Contains(car2, availableCars);
            Assert.Contains(car3, availableCars);
            Assert.DoesNotContain(rentedCar, availableCars);
        }

        //[Fact]
        //public void GetAvailableCars_NoRentedCars_ReturnsAllCars()
        //{
        //    // Arrange
        //    var repository = Substitute.For<IRepository>();
        //    var rentServices = new RentServices(repository);

        //    var day = new DateTime(2023, 8, 17);
        //    var location = "LocationA";

        //    var car1 = new Car { Manufacturer = "Toyota", Model = "Corolla", Color = "Blue", KW = 150 };
        //    var car2 = new Car { Manufacturer = "Honda", Model = "Civic", Color = "Red", KW = 120 };
        //    var car3 = new Car { Manufacturer = "Ford", Model = "Focus", Color = "Green", KW = 130 };

        //    repository.GetAll<Car>().Returns(new List<Car> { car1, car2, car3 });

        //    // No rented cars
        //    repository.GetAll<Rent>().Returns(new List<Rent>());

        //    // Act
        //    var availableCars = rentServices.GetAvailableCars(day, location);

        //    // Assert
        //    Assert.Contains(car1, availableCars);
        //    Assert.Contains(car2, availableCars);
        //    Assert.Contains(car3, availableCars);
        //}

        //[Fact]
        //public void GetAvailableCars_AllCarsRented_ReturnsNoAvailableCars()
        //{
        //    // Arrange
        //    var repository = Substitute.For<IRepository>();
        //    var rentServices = new RentServices(repository);

        //    var day = new DateTime(2023, 8, 17);
        //    var location = "LocationA";

        //    var car1 = new Car { Manufacturer = "Toyota", Model = "Corolla", Color = "Blue", KW = 150 };
        //    var car2 = new Car { Manufacturer = "Honda", Model = "Civic", Color = "Red", KW = 120 };
        //    var car3 = new Car { Manufacturer = "Ford", Model = "Focus", Color = "Green", KW = 130 };

        //    // All cars are rented for the specified day and location
        //    repository.GetAll<Car>().Returns(new List<Car> { car1, car2, car3 });
        //    var cust = new Customer() { Name = "Fred" };

        //    repository.GetAll<Rent>().Returns(new List<Rent>
        //    {
        //        new Rent {Customer=cust, Car = car1, StartDate = day, EndDate = day.AddDays(1), StartLocation = location },
        //        new Rent {Customer=cust, Car = car2, StartDate = day, EndDate = day.AddDays(1), StartLocation = location },
        //        new Rent {Customer=cust, Car = car3, StartDate = day, EndDate = day.AddDays(1), StartLocation = location }
        //    });

        //    // Act
        //    var availableCars = rentServices.GetAvailableCars(day, location);

        //    // Assert
        //    Assert.Empty(availableCars);
        //}
    }
}
