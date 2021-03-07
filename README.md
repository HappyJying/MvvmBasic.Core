# Getting Started

```csharp
using MvvmBasic.Core;

public class MainViewModel : Observable
{
    // Value Binding
    private string _title;
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }

    // Command Binding
    private RelayCommand _hello;
    public RelayCommand Hello => _hello ?? (_hello = new RelayCommand(OnHello));

    public MainViewModel()
    {
        // Subscribe Message
        Messager.Subscribe(Messages.Alert, m =>
        {
            System.Windows.MessageBox.Show((string)m[0]);
        });
        
        // Subscribe Message On UI Thread
        Messager.Subscribe(Messages.Alert, m =>
        {
            System.Windows.MessageBox.Show((string)m[0]);
        }, true);
    }

    private void OnHello()
    {
        // Publish Message
        Messager.Publish(Messages.Alert, "Hello");

        // Unsubscribe Message
        Messager.Unsubscribe(Messages.Alert);
    }
}
```
