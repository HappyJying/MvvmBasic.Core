using System.Windows;
using System.Windows.Controls;
using Example.Desktop.Contracts.Views;
using Example.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MvvmBasic.Core;

namespace Example.Desktop.Views
{
    public partial class ShellWindow : MetroWindow, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Messager.Subscribe(Messages.Alert, m => MessageBox.Show((string)m[0]));
        }

        public Frame GetNavigationFrame()
            => shellFrame;

        public void ShowWindow()
            => Show();

        public void CloseWindow()
            => Close();
    }
}
