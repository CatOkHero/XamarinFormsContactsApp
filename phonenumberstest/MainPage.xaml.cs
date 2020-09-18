using System.Threading.Tasks;
using phonenumberstest.ViewModels;
using Xamarin.Forms;

namespace phonenumberstest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var viewModel = new MainPageViewModel();
            Task.Run(async () => await viewModel.Initialize());
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
