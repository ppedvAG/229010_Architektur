using ppedv.CarRentalXPress.Model.Contracts;
using System.Windows.Input;

namespace ppedv.CarRentalXPress.UI.Desktop.ViewModels
{
    public class SaveCommand : ICommand
    {
        private readonly IUnitOfWork uow;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public SaveCommand(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Execute(object? parameter)
        {
            uow.SaveAll();
        }
    }
}
