# Get Started

Step 1: Define ViewModel.

```csharp
public class MainViewModel : Observable { }
```

Step 2: Command Binding.

```csharp
public RelayCommand Hello => _hello ?? (_hello = new RelayCommand(OnHello));

private void OnHello() { };
```

# Use Messager

Step 1: Define Message Constants.

```csharp
public enum Messages { Alert }
```

Step 2: Subscribe Message Event.

```csharp
Messager.Subscribe(Messages.Alert, m => MessageBox.Show((string)m[0]));
```

Subscribe On UI Thread:

```csharp
Messager.Subscribe(Messages.Alert, m => MessageBox.Show((string)m[0]), true);
```

Step 3: Publish Message Event.

```csharp
Messager.Publish(Messages.Alert, "Hello");
```
