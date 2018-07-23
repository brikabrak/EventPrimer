using System;
using System.Collections.Generic;
using someEventTest.Components;

namespace someEventTest
{
    class Program
    {
        static int id = 0;

        static void Main(string[] args)
        {
            // main object - represents player
            Dictionary<string, Component> components = new Dictionary<string, Component>();
            components.Add("health", new Health(id++, 255));
            var obj = new GameObject(id++, components);
            obj.Init();

            // Other object - collider, so think projectile or something
            components = new Dictionary<string, Component>();
            components.Add("damage", new Damage(id++, 35));
            var other = new GameObject(id++, components);
            other.Init();

            // Setup Text Writer for updating status - Basic UI tie-in
            var c = new TextWriter(id++, "Player health is now {0}");
            obj.GetComponent("health").AddSubscriber(c.OnModifier);

            components = new Dictionary<string, Component>();
            components.Add("uiUpdate", c);
            var writer = new GameObject(id++, components);
            writer.Init();

            for(var i = 0; i < 4; i++){
                obj.Collide(other);
            }

            other.Collide(obj);
            
            obj.Dispose();
            other.Dispose();
            writer.Dispose();
        }
    }
}
