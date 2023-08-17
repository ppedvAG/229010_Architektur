using Microsoft.Extensions.DependencyInjection;
using ppedv.CarRentalXPress.UI.Desktop.ViewModels;
using System.Windows.Controls;

namespace ppedv.CarRentalXPress.UI.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CarsView.xaml
    /// </summary>
    public partial class CarsView : UserControl
    {
        public CarsView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<CarsViewModel>();
        }
    }
}
