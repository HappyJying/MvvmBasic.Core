using MvvmBasic.Core;

namespace Example.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Messager.Subscribe(Messages.Alert, m => DisplayAlert("", (string)m[0], "确定"));
        }
    }
}
