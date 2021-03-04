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
        public RelayCommand Hello => _hello ?? (_hello = new RelayCommand(OnHello));

        public MainViewModel()
        {
            for (int i = 0; i < 26; i++)
            {
                var item = new Item
                {
                    Id = i + 1,
                    Name = ((char)('A' + i)).ToString(),
                };
                Items.Add(item);
            }
        }

        private void OnHello()
        {
            Messager.Publish(Messages.Alert, "Hello");
            Messager.Unsubscribe(Messages.Alert);
            Task.Run(() => Messager.Publish(Messages.Scroll));
        }
    }
}
