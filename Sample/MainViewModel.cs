using MvvmBasic.Core;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Sample
{
    internal class MainViewModel : Observable
    {
        private string _title;

        private RelayCommand? _subscribeMessage;
        private RelayCommand? _subscribeMessageOnUIThread;
        private RelayCommand? _subscribeMessageWithResult;
        private RelayCommand? _unsubscribeMessage;
        private RelayCommand? _publishMessage;
        private RelayCommand? _publishMessageOnBackgroundThread;
        private RelayCommand? _publishMessageForResult;
        private RelayCommand? _clearMessageList;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public ObservableCollection<string> MessageList { get; } = new();

        public RelayCommand SubscribeMessage => _subscribeMessage ??= new RelayCommand(OnSubscribeMessage);
        public RelayCommand SubscribeMessageOnUIThread => _subscribeMessageOnUIThread ??= new RelayCommand(OnSubscribeMessageOnUIThread);
        public RelayCommand SubscribeMessageWithResult => _subscribeMessageWithResult ??= new RelayCommand(OnSubscribeMessageWithResult);
        public RelayCommand UnsubscribeMessage => _unsubscribeMessage ??= new RelayCommand(OnUnsubscribeMessage);
        public RelayCommand PublishMessage => _publishMessage ??= new RelayCommand(OnPublishMessage);
        public RelayCommand PublishMessageOnBackgroundThread => _publishMessageOnBackgroundThread ??= new RelayCommand(OnPublishMessageOnBackgroundThread);
        public RelayCommand PublishMessageForResult => _publishMessageForResult ??= new RelayCommand(OnPublishMessageForResult);
        public RelayCommand ClearMessageList => _clearMessageList ??= new RelayCommand(OnClearMessageList);

        public MainViewModel()
        {
            _title = "Sample";
        }

        private void OnSubscribeMessage()
        {
            Messager.Subscribe(Messages.AddMessage, args =>
            { 
                if (args[0] is string messageId)
                {
                    try
                    {
                        MessageList.Add($"[Subscribe] [{messageId}] The message event method has been called.");
                    }
                    catch (NotSupportedException ex)
                    {
                        throw new Exception("Please subscribe or publish the message on the UI thread.", ex);
                    }
                }
            });

            MessageList.Add("[Subscribe] The message has been subscribed.");
        }

        private void OnSubscribeMessageOnUIThread()
        {
            Messager.Subscribe(Messages.AddMessage, args =>
            {
                if (args[0] is string messageId)
                {
                    MessageList.Add($"[Subscribe] [{messageId}] The message event method has been called.");
                }
            }, true);

            MessageList.Add("[Subscribe] The message has been subscribed on the UI thread.");
        }

        private void OnSubscribeMessageWithResult()
        {
            Messager.Subscribe(Messages.AddMessage, args =>
            {
                if (args[0] is string messageId)
                {
                    MessageList.Add($"[Subscribe] [{messageId}] The message event method has been called.");
                    return true;
                }
                return false;
            });

            MessageList.Add("[Subscribe] The message has been subscribed with a return value.");
        }

        private void OnPublishMessage()
        {
            string messageId = DateTime.Now.Ticks.ToString();
            Messager.Publish(Messages.AddMessage, messageId);

            MessageList.Add($"[Publish] [{messageId}] The message has been published.");
        }

        private void OnPublishMessageOnBackgroundThread()
        {
            string messageId = DateTime.Now.Ticks.ToString();
            Task.Run(() =>
            {
                Messager.Publish(Messages.AddMessage, messageId);
            });

            MessageList.Add($"[Publish] [{messageId}] The message has been published on a background thread.");
        }

        private void OnPublishMessageForResult()
        {
            string messageId = DateTime.Now.Ticks.ToString();
            bool result = Messager.Publish<bool>(Messages.AddMessage, messageId);
            MessageList.Add($"[Publish] [{messageId}] Return value: {result}.");

            MessageList.Add($"[Publish] [{messageId}] The message has been published for a return value.");
        }

        private void OnUnsubscribeMessage()
        {
            Messager.Unsubscribe(Messages.AddMessage);

            MessageList.Add("[Unsubscribe] The message has been unsubscribed.");
        }

        private void OnClearMessageList()
        {
            MessageList.Clear();
        }

    }
}
