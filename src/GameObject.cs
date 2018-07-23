using System;
using System.Collections.Generic;
using someEventTest.Components;
using someEventTest.Event;

namespace someEventTest
{
    public interface IGameObject
    {
        void Init();
        void Update();
    }

    public class GameObject : IGameObject, IDisposable
    {
        public readonly int Id;

        public event EventHandler<EventArgs> OnAttack;  // Events, sometimes triggered from other components
        public event EventHandler<EventArgs> OnCollision;
        public event EventHandler<EventArgs> OnHeal;

        private Dictionary<string, Component> components;
        private bool disposed;

        public GameObject(int id, Dictionary<string, Component> dictionary) {
            Id = id;
            components = dictionary;
        }

        ~GameObject() {
            Dispose(false);
            Console.WriteLine(String.Format("GameObject {0} disposed finally.", Id.ToString()));
        }

        public void Init() {
            foreach(var c in components) {
                // Pretty hard coded. Assumes those components would care about those events
                // At least if they don't, they will just run empty calls, but dat overhead
                OnAttack += c.Value.OnAttack;
                OnCollision += c.Value.OnCollision;
                OnHeal += c.Value.OnHeal;
            }
        }

        public void Update() { }

        // Represents a collision event happening
        public void Collide(GameObject other) {
            if(OnCollision != null) {
                // What if no damage on other object?
                // Also probably X other component combinations here
                var c = (Damage) other.GetComponent("damage");

                OnCollision(this, new ValueUpdateEventArgs(c != null ? c.Power : 0));
            }
        }

        public Component GetComponent(string type)
        {
            Component c = null;
            if(components.TryGetValue(type, out c)) {
                return c;
            }

            return c;
        }

        public void Dispose(){
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if(disposed){
                return;
            }

            if(disposing) {
                OnAttack = null;
                OnCollision = null;
                OnHeal = null;

                components.Clear();
            }

            disposed = true;
        }
    }
}