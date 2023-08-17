
namespace ppedv.CarRentalXPress.Model.Contracts
{
    public interface IRentServices
    {
        void EndRent(Rent rent, string location);
        IEnumerable<Car> GetAvailableCars(DateTime day, string location);
        void StartRent(Rent rent, string location);
    }
}