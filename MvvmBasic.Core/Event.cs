using System;
using System.Threading;

namespace MvvmBasic.Core
{
    public class Event
    {
        public object Message { get; set; }

        public Action<object[]> Action { get; set; }

        public Func<object[], object> Func { get; set; }

        public bool OnUIThread { get; set; }

        public SynchronizationContext Context { get; set; }

        public Event(object message, Action<object[]> action, bool onUIThread = false)
        {
            Message = message;
            Action = action;
            OnUIThread = onUIThread;

            if (OnUIThread)
            {
                Context = SynchronizationContext.Current;
            }
        }

        public Event(object message, Func<object[], object> func)
        {
            Message = message;
            Func = func;
        }
    }
}
