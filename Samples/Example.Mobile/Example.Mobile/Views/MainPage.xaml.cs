using Example.ViewModels;
using MvvmBasic.Core;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Example.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var viewModel = DependencyService.Get<MainViewModel>();
            BindingContext = viewModel;
            Messager.Subscribe(Messages.Scroll, m => ListView.ScrollTo(viewModel.Items.Last(), ScrollToPosition.MakeVisible, true));
        }
    }
}