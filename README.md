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
    private RelayCommand _click;
    public RelayCommand Click => _click ?? (_click = new RelayCommand(OnClick));

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

    private void OnClick()
    {
        // Publish Message
        Messager.Publish(Messages.Alert, "Hello World!");

        // Unsubscribe Message
        Messager.Unsubscribe(Messages.Alert);
    }
}
```
