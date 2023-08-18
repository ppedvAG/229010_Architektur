using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using ppedv.CarRentalXPress.Model;
using ppedv.CarRentalXPress.Model.Contracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ppedv.CarRentalXPress.UI.Desktop.ViewModels
{
    public class CarsViewModel : ObservableObject
    {
        private readonly IUnitOfWork unitOfWork;
        private Car selectedCar;

        public CarsViewModel(IUnitOfWork uow, IRentServices rentService)
        {
            this.unitOfWork = uow;
            CarList = new ObservableCollection<Car>(uow.CarRepository.GetAll());

            //SaveCommand = new SaveCommand(repo);
            SaveCommand = new RelayCommand(() =>
            {
                uow.SaveAll();
                uow = App.Current.Services.GetService<IUnitOfWork>();
            });
            NewCommand = new RelayCommand(UserWantsToAddNewCar);
            DeleteCommand = new RelayCommand(UserWantsToDeleteSelectedCar);

            //IRentServices rentService = App.Current.Services.GetService<IRentServices>();
            ShowOnlyAvailableCarsCommand = new RelayCommand(() =>
            {
                CarList.Clear();
                rentService.GetAvailableCars(new DateTime(2023, 10, 11), "Heidelberg").ToList().ForEach(car => CarList.Add(car));
            });
        }

        private void UserWantsToDeleteSelectedCar()
        {
            if (selectedCar != null)
            {
                unitOfWork.CarRepository.Delete(selectedCar);
                CarList.Remove(selectedCar);
            }
        }

        private void UserWantsToAddNewCar()
        {
            var car = new Car() { Color = "NEU", Manufacturer = "NEU", Model = "NEU", KW = 12 };
            unitOfWork.CarRepository.Add(car);
            CarList.Add(car);
            SelectedCar = car;
        }


        public ObservableCollection<Car> CarList { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand ShowOnlyAvailableCarsCommand { get; set; }

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
