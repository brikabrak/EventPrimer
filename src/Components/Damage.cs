namespace someEventTest.Components
{
    public class Damage : Component {
        public readonly int Power;

        public Damage(int id, int damage) : base(id) {
            Power = damage;
        }
    }
}