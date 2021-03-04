using System.Windows.Controls;
using Example.Shared;
using Example.ViewModels;
using MvvmBasic.Core;

namespace Example.Desktop.Views
{
    public partial class MainPage : Page
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Messager.Subscribe(Messages.Scroll, m => DataGrid.ScrollIntoView(viewModel.Items[^1]), true);
        }
    }
}
