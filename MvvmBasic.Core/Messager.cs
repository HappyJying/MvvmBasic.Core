using System;
using System.Collections.Generic;

namespace MvvmBasic.Core
{
    public static class Messager
    {
        private static readonly List<Event> events = new List<Event>();

        public static void Publish(object message, params object[] args)
        {
            List<Event> es = events.FindAll(a => a.Action != null && Equals(a.Message, message));
            foreach (Event e in es)
            {
                if (e.OnUIThread)
                {
                    e.Context?.Post(o => e.Action((object[])o), args);
                }
                else
                {
                    e.Action.Invoke(args);
                }
            }
        }

        public static T Publish<T>(object message, params object[] args)
        {
            Event e = events.Find(a => a.Func != null && Equals(a.Message, message));
            if (e == null)
            {
                return default;
            }
            return (T)e.Func.Invoke(args);
        }

        public static void Subscribe(object message, Action<object[]> action, bool onUIThread = false)
        {
            if (!events.Exists(a => Equals(a.Message, message) && a.Action == action && a.OnUIThread == onUIThread))
            {
                events.Add(new Event(message, action, onUIThread));
            }
        }

        public static void Subscribe(object message, Func<object[], object> func)
        {
            if (!events.Exists(a => Equals(a.Message, message) && a.Func == func))
            {
                events.Add(new Event(message, func));
            }
        }

        public static void Unsubscribe(object message)
        {
            events.RemoveAll(a => Equals(a.Message, message));
        }

    }
}
