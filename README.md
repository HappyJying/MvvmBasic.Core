# Getting Started

```c#
using MvvmBasic.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// message constants definition
internal enum Messages
{
    AddMessage
}

internal class MainViewModel : Observable
{
    // value binding
    private string _text;
    public string Text
    {
        get => _text;
        set => Set(ref _text, value);
    }

    // collection binding
    public ObservableCollection<string> MessageList { get; } = new();

    // command binding
    private RelayCommand _test;
    public RelayCommand Test => _test ??= new RelayCommand(OnTest);
    private void OnTest() {}

    public MainViewModel()
    {
        // subscribe a normal message
        Messager.Subscribe(Messages.AddMessage, args =>
        { 
            if (args[0] is string message)
            {
                MessageList.Add(message);
            }
        });

        // subscribe a message whose event method can be called on the UI thread
        Messager.Subscribe(Messages.AddMessage, args =>
        {
            if (args[0] is string message)
            {
                MessageList.Add(message);
            }
        }, true);
        
        // subscribe a message with a return value
        Messager.Subscribe(Messages.AddMessage, args =>
        {
            if (args[0] is string message)
            {
                MessageList.Add(message);
                return true;
            }
            return false;
        });

        // publish a normal message
        Messager.Publish(Messages.AddMessage, "Hello, World!");

        // publish a message on a background thread
        Task.Run(() =>
        {
            Messager.Publish(Messages.AddMessage, "Hello, World!");
        });

        // publish a message for a return value
        bool result = Messager.Publish<bool>(Messages.AddMessage, "Hello, World!");

        // unsubscribe a message
        Messager.Unsubscribe(Messages.AddMessage);
    }

}
```
