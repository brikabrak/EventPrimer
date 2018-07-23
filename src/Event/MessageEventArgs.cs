using System;

namespace someEventTest.Event
{
    public class MessageEventArgs : EventArgs
    {
        public readonly string Message;

        public MessageEventArgs(string message) {
            Message = message;
        }
    }
}