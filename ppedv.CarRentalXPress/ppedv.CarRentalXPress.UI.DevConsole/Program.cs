using Autofac;
using ppedv.CarRentalXPress.Core;
using ppedv.CarRentalXPress.Data.EfCore;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;
using System.Globalization;
using System.Reflection;

Console.WriteLine("*** CarRentalXPress v0.1 ***");

Thread.CurrentThread.CurrentCulture = new CultureInfo("de");

string conString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true";

//DI per Reflection
//var filePath = @"C:\Users\Fred\source\repos\ppedvAG\229010_Architektur\ppedv.CarRentalXPress\ppedv.CarRentalXPress.Data.EfCore\bin\Debug\net7.0\ppedv.CarRentalXPress.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(filePath);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = Activator.CreateInstance(typeMitRepo, conString) as IRepository;

//DI per AutoFac
var containerBuilder = new ContainerBuilder();
containerBuilder.Register(x => new CarRentalXPressContextUnitOfWorkAdapter(conString)).As<IUnitOfWork>();
containerBuilder.RegisterType<RentServices>().As<IRentServices>();
var container = containerBuilder.Build();

IUnitOfWork uow = container.Resolve<IUnitOfWork>();

//manual injection
//IRepository repo = new ppedv.CarRentalXPress.Data.EfCore.CarRentalXPressContextRepositoryAdapter(conString);
//var rentService = new RentServices(repo);
var rentService = container.Resolve<IRentServices>();

var demoService = new DemoService(uow);
//demoService.CreateDemoDaten();

 //uow.CarRepository.GetAllTheSpecialCars();

Console.WriteLine("All Cars:");
foreach (var car in uow.CarRepository.Query().Where(x => x.KW > 5).OrderBy(x => x.Color).ToList())
{
    Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Color} {car.KW}");
    foreach (var r in car.Rents.OrderBy(x => x.StartDate))
    {
        Console.WriteLine($"\t{r.StartDate.Date:d} [{r.StartLocation}] - {r.EndDate.Date:d} [{r.EndLocation}] {r.Customer.Name}");
    }
}

Console.WriteLine("Available Cars today:");
foreach (var car in rentService.GetAvailableCars(DateTime.Now.AddDays(3), "Heidelberg"))
{
    Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Color} {car.KW}");
}

Console.WriteLine("Ende");
Console.ReadLine();
