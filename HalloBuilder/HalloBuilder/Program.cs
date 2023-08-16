// See https://aka.ms/new-console-template for more information
using HalloBuilder;

Console.WriteLine("Hello, World!");

var s1 = new Schrank.Builder()
                    .SetTüren(4)
                    .SetBöden(4)
                    .Build();

var s2 = new Schrank.Builder()
                    .SetTüren(4)
                    .SetBöden(4)
                    .SetOberfläche(Oberfläche.Lackiert)
                    .SetFarbe("blau")
                    .Build();