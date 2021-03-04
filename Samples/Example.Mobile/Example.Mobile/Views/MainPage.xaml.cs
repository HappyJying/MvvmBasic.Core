using Example.Shared;
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
        private readonly MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = DependencyService.Resolve<MainViewModel>();
            BindingContext = viewModel;
            Messager.Subscribe(Messages.Scroll, m => ListView.ScrollTo(viewModel.Items.Last(), ScrollToPosition.MakeVisible, true));
        }
    }
}