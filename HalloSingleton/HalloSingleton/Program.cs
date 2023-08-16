using HalloSingleton;

Console.WriteLine("Hello, World!");



for (int i = 0; i < 10; i++)
{
    Task.Run(() => Logger.Instance.Info("Moin"));
 
}

Logger.Instance.Error("PANiKK!!!11");