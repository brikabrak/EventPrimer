using System;

namespace someEventTest.Event
{
    public class SubscriberEventArgs : EventArgs
    {
        public enum SubscriberAction : byte {
            Enable = 0,
            Disable,
            Update
        }

        public readonly EventHandler<EventArgs> SubscribingEvent;
        public readonly SubscriberAction Action;

        public SubscriberEventArgs(SubscriberAction action, EventHandler<EventArgs> e) {
            Action = action;
            SubscribingEvent = e;
        }
    }
}