using System;
using someEventTest.Event;

namespace someEventTest.Components
{
    public class TextWriter : Component
    {
        private string baseMessage;

        public TextWriter(int id, string message) : base(id) {
            baseMessage = message;
        }

        public override void OnModifier(object sender, EventArgs args) {
            var a = (MessageEventArgs) args;

            Console.WriteLine(String.Format(baseMessage, a.Message));
        }
    }
}