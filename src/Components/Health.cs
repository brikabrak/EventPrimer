using System;
using someEventTest.Event;

namespace someEventTest.Components
{
    public class Health : Component
    {
        public event EventHandler<EventArgs> OnUpdate;

        public int MaximumValue;

        private int value;

        public Health(int id, int maxHealth) : base(id) {
            
            MaximumValue = maxHealth;
            value = MaximumValue;
        }

        public override void OnCollision(object sender, EventArgs args) {
            var d = (ValueUpdateEventArgs) args;
            
            value -= d.NewValue;

            OnUpdate(this, new MessageEventArgs(value.ToString()));
        }

        public override void AddSubscriber(EventHandler<EventArgs> e) {
            OnUpdate += e;
        }

        public override void RemoveSubscriber(EventHandler<EventArgs> e) {
            OnUpdate -= e;
        }

        protected override void Dispose(bool disposing) {
            if(disposed) {
                return;
            }

            if(disposing) {
                OnUpdate = null;
            }
            
            disposed = true;
            base.Dispose(disposing);
        }
    }
}