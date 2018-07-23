using System;
using someEventTest.Event;

namespace someEventTest.Components
{
    public interface IComponent {
        void OnAttack(object sender, EventArgs args);
        void OnCollision(object sender, EventArgs args);
        void OnHeal(object sender, EventArgs args);
        void OnModifier(object sender, EventArgs args);
        void OnSubscriberUpdate(object sender, EventArgs args);
    }

    public abstract class Component : IComponent, IDisposable {
        public readonly int Id;

        protected bool disposed = false;

        public Component(int id) {
            Id = id;
        }

        ~Component() {
            Dispose(false);
            Console.WriteLine(String.Format("Component {0} disposed finally.", Id.ToString()));
        }

        public virtual void OnAttack(object sender, EventArgs args) { }
        public virtual void OnCollision(object sender, EventArgs args) { }
        public virtual void OnHeal(object sender, EventArgs args) { }
        public virtual void OnModifier(object sender, EventArgs args) { }

        public virtual void OnSubscriberUpdate(object sender, EventArgs args) {
            var a = (SubscriberEventArgs) args;

            if (a.SubscribingEvent != null) {
                switch(a.Action) {
                    case SubscriberEventArgs.SubscriberAction.Disable:
                        RemoveSubscriber(a.SubscribingEvent);
                        break;
                    case SubscriberEventArgs.SubscriberAction.Enable:
                        AddSubscriber(a.SubscribingEvent);
                        break;
                    default:
                        UpdateSubscriber(a.SubscribingEvent);
                        break;
                }
            }
        }

        public virtual void AddSubscriber(EventHandler<EventArgs> e) { }
        public virtual void RemoveSubscriber(EventHandler<EventArgs> e) { }
        public virtual void UpdateSubscriber(EventHandler<EventArgs> e) { }

        public void Dispose() {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(disposed) {
                return;
            }

            disposed = true;
        }
    }
}