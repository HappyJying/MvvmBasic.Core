using System;
using System.Collections.Generic;

namespace MvvmBasic.Core
{
    public static class Messager
    {
        private static readonly List<Event> events = new List<Event>();

        /// <summary>
        /// Publish a message.
        /// </summary>
        public static void Publish(object message, params object[] args)
        {
            List<Event> es = events.FindAll(a => Equals(a.Message, message));
            foreach (Event e in es)
            {
                if (e.Action != null)
                {
                    if (e.OnUIThread)
                    {
                        e.Context.Post(o => e.Action((object[])o), args);
                    }
                    else
                    {
                        e.Action.Invoke(args);
                    }
                }
                else if (e.Func != null)
                {
                    if (e.OnUIThread)
                    {
                        e.Context.Post(o => e.Func((object[])o), args);
                    }
                    else
                    {
                        e.Func.Invoke(args);
                    }
                }
            }
        }

        /// <summary>
        /// Publish a message for a return value.
        /// </summary>
        public static T Publish<T>(object message, params object[] args)
        {
            Event e = events.Find(a => a.Func != null && Equals(a.Message, message));
            if (e == null)
            {
                return default;
            }

            if (e.OnUIThread)
            {
                T t = default;
                bool isCompleted = false;

                e.Context.Post(o =>
                {
                    t = (T)e.Func((object[])o);
                    isCompleted = true;
                }, args);

                while (!isCompleted) { }
                return t;
            }
            else
            {
                return (T)e.Func.Invoke(args);
            }
        }

        /// <summary>
        /// Subscribe to a message.
        /// </summary>
        /// <remarks>
        /// <typeparamref name="onUIThread"/>: true to make the event called on the UI thread; otherwise, false.
        /// </remarks>
        public static void Subscribe(object message, Action<object[]> action, bool onUIThread = false)
        {
            if (!events.Exists(a => Equals(a.Message, message) && a.Action == action && a.OnUIThread == onUIThread))
            {
                events.Add(new Event(message, action, onUIThread));
            }
        }

        /// <summary>
        /// Subscribe to a message with a return value.
        /// </summary>
        /// <remarks>
        /// <typeparamref name="onUIThread"/>: true to make the event called on the UI thread; otherwise, false.
        /// </remarks>
        public static void Subscribe(object message, Func<object[], object> func, bool onUIThread = false)
        {
            if (!events.Exists(a => Equals(a.Message, message) && a.Func == func && a.OnUIThread == onUIThread))
            {
                events.Add(new Event(message, func, onUIThread));
            }
        }

        /// <summary>
        /// Unsubscribe from a message.
        /// </summary>
        public static void Unsubscribe(object message)
        {
            events.RemoveAll(a => Equals(a.Message, message));
        }

    }
}
