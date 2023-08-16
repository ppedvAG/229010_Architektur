// See https://aka.ms/new-console-template for more information
using Decorator;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Hello, World!");

var p1 = new Käse(new Salami(new Käse(new Pizza())));

Console.WriteLine($"{p1.Name} {p1.Price:c}");