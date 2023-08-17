using Bogus;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;

namespace ppedv.CarRentalXPress.Core
{
    public class DemoService
    {
        private readonly IRepository repo;

        public DemoService(IRepository repo)
        {
            this.repo = repo;
        }

        public void CreateDemoDaten()
        {
            var cars = GenerateCars(10);
            var customers = GenerateCustomers(5);
            var rents = GenerateRents(cars, customers, 20);
            foreach (var r in rents)
            {
                repo.Add(r);
            }
            repo.SaveAll();
        }

        const string local = "de";
        const int seed = 7;

        public static IEnumerable<Car> GenerateCars(int count)
        {
            var carFaker = new Faker<Car>(local).UseSeed(seed)
                .RuleFor(c => c.Manufacturer, f => f.Vehicle.Manufacturer())
                .RuleFor(c => c.Model, f => f.Vehicle.Model())
                .RuleFor(c => c.Color, f => f.Commerce.Color())
                .RuleFor(c => c.KW, f => f.Random.Int(50, 300));

            return carFaker.Generate(count);
        }

        public static IEnumerable<Customer> GenerateCustomers(int count)
        {
            var customerFaker = new Faker<Customer>(local).UseSeed(seed)
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.BirthDate, f => f.Date.Past(20));

            return customerFaker.Generate(count);
        }

        public static IEnumerable<Rent> GenerateRents(IEnumerable<Car> cars, IEnumerable<Customer> customers, int count)
        {
            var rentFaker = new Faker<Rent>(local).UseSeed(seed)
                .RuleFor(r => r.OrderDate, f => f.Date.Past())
                .RuleFor(r => r.StartDate, f => f.Date.Future())
                .RuleFor(r => r.StartLocation, f => f.Address.City())
                .RuleFor(r => r.EndDate, (f, r) => f.Date.Between(r.StartDate, r.StartDate.AddDays(7)))
                .RuleFor(r => r.EndLocation, f => f.Address.City())
                .RuleFor(r => r.Car, f => f.PickRandom(cars))
                .RuleFor(r => r.Customer, f => f.PickRandom(customers));

            return rentFaker.Generate(count);
        }


    }
}
