using System;

namespace someEventTest.Event
{
    public class ValueUpdateEventArgs : EventArgs
    {
        public readonly int NewValue;

        public ValueUpdateEventArgs(int newValue) {
            NewValue = newValue;
        }
    }
}