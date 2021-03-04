using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Example.Shared;
using MvvmBasic.Core;

namespace Example.ViewModels
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();

        private RelayCommand _hello;
        public RelayCommand Hello => _hello ??= new RelayCommand(OnHello);

        public MainViewModel()
        {
            var items = new Item[26];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Item
                {
                    Id = i + 1,
                    Name = ((char)('A' + i)).ToString(),
                };
            }
            Items.AddRange(items);
        }

        private void OnHello()
        {
            Messager.Publish(Messages.Alert, "Hello");
            Messager.Unsubscribe(Messages.Alert);
            Task.Run(() => Messager.Publish(Messages.Scroll));
        }
    }
}
