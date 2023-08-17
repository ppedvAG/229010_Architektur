using Autofac;
using ppedv.CarRentalXPress.Core;
using ppedv.CarRentalXPress.Data.EfCore;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;
using System.Reflection;

Console.WriteLine("*** CarRentalXPress v0.1 ***");

string conString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true";

//DI per Reflection
//var filePath = @"C:\Users\Fred\source\repos\ppedvAG\229010_Architektur\ppedv.CarRentalXPress\ppedv.CarRentalXPress.Data.EfCore\bin\Debug\net7.0\ppedv.CarRentalXPress.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(filePath);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = Activator.CreateInstance(typeMitRepo, conString) as IRepository;

//DI per AutoFac
var containerBuilder = new ContainerBuilder();
containerBuilder.Register(x => new CarRentalXPressContextRepositoryAdapter(conString)).As<IRepository>();
var container = containerBuilder.Build();

IRepository repo = container.Resolve<IRepository>();

//manual injection
//IRepository repo = new ppedv.CarRentalXPress.Data.EfCore.CarRentalXPressContextRepositoryAdapter(conString);
var rentService = new RentServices(repo);



Console.WriteLine("All Cars:");
foreach (var car in repo.GetAll<Car>())
{
    Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Color} {car.KW}");
}

Console.WriteLine("Available Cars today:");
foreach (var car in rentService.GetAvailableCars(DateTime.Now, "Heidelberg"))
{
    Console.WriteLine($"{car.Manufacturer} {car.Model} {car.Color} {car.KW}");
}

Console.WriteLine("Ende");
Console.ReadLine();
