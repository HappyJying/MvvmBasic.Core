using Example.Shared;
using MvvmBasic.Core;

namespace Example.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Messager.Subscribe(Messages.Alert, m => DisplayAlert("", "Hello", "确定"));
        }
    }
}
