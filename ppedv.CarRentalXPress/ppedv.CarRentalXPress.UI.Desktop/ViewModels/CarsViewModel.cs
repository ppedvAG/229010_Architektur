using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.CarRentalXPress.UI.Desktop.ViewModels
{
    public class CarsViewModel : ObservableObject
    {
        private readonly IRepository repo;
        private Car selectedCar;

        public CarsViewModel(IRepository repo)
        {
            this.repo = repo;
            CarList = new ObservableCollection<Car>(repo.GetAll<Car>());

            //SaveCommand = new SaveCommand(repo);
            SaveCommand = new RelayCommand(() => repo.SaveAll());
            NewCommand = new RelayCommand(UserWantsToAddNewCar);
            DeleteCommand = new RelayCommand(UserWantsToDeleteSelectedCar);
        }

        private void UserWantsToDeleteSelectedCar()
        {
            if(selectedCar!=null)
            {
                repo.Delete(selectedCar);
                CarList.Remove(selectedCar);
            }
        }

        private void UserWantsToAddNewCar()
        {
            var car = new Car() { Color = "NEU", Manufacturer = "NEU", Model = "NEU", KW = 12 };
            repo.Add(car);
            CarList.Add(car);
            SelectedCar = car;
        }

        //todo KILL IT
        public CarsViewModel() : this(new Data.EfCore.CarRentalXPressContextRepositoryAdapter("Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true"))
        { }

        public ObservableCollection<Car> CarList { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCommand { get; set; }

        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                selectedCar = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PS)));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCar)));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                OnPropertyChanged(nameof(PS));
                OnPropertyChanged(nameof(KW));
                OnPropertyChanged(nameof(SelectedCar));

            }
        }

        public int KW
        {
            get => selectedCar == null ? -1 : selectedCar.KW;
            set
            {
                if (selectedCar != null)
                    selectedCar.KW = value;

                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("KW"));
                OnPropertyChanged("KW");
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PS"));
                OnPropertyChanged("PS");
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
