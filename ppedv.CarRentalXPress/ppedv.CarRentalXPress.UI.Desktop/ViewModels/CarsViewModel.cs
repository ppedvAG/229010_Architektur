using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;
using System.ComponentModel;

namespace ppedv.CarRentalXPress.UI.Desktop.ViewModels
{
    public class CarsViewModel : INotifyPropertyChanged
    {
        private readonly IRepository repo;
        private Car selectedCar;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CarsViewModel(IRepository repo)
        {
            this.repo = repo;
            CarList = new List<Car>(repo.GetAll<Car>());
        }

        //todo KILL IT
        public CarsViewModel() : this(new Data.EfCore.CarRentalXPressContextRepositoryAdapter("Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true"))
        { }

        public List<Car> CarList { get; set; }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PS)));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCar)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));

            }
        }

        public int KW
        {
            get => selectedCar == null ? -1 : selectedCar.KW;
            set
            {
                if (selectedCar != null)
                    selectedCar.KW = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KW"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PS"));
           }
        }

        public string PS
        {
            get
            {
                if (SelectedCar == null)
                    return "---";

                return (SelectedCar.KW * 1.36).ToString("0.00");
            }
        }
    }
}
