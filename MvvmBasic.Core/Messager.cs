using System;
using System.Collections.Generic;

namespace MvvmBasic.Core
{
    public static class Messager
    {
        private static readonly List<Event> events = new List<Event>();

        public static void Publish(object message, params object[] obj)
        {
            var events = FindAll(message);
            events.ForEach(e =>
            {
                if (e == null || e.Action == null) return;
                if (!e.OnUIThread) e.Action.Invoke(obj);
                else e.Context?.Post((o) => e.Action((object[])o), obj);
            });
        }

        public static void Subscribe(object message, Action<object[]> action, bool onUIThread = false)
        {
            if (!Exists(message, action, onUIThread)) events.Add(new Event(message, action, onUIThread));
        }

        public static void Unsubscribe(object message)
        {
            events.RemoveAll(a => Equals(a.Message, message));
        }

        private static bool Exists(object message, Action<object[]> action, bool onUIThread = false)
        {
            return events.Exists(a => Equals(a.Message, message) && a.Action == action && a.OnUIThread == onUIThread);
        }

        private static List<Event> FindAll(object message)
        {
            return events.FindAll(a => Equals(a.Message, message));
        }
    }
}
