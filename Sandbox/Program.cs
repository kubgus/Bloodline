using BloodlineEngine;
using Microsoft.VisualBasic.Logging;

namespace Sandbox
{
    public class TestScript : Component
    {
        public override void Ready()
        {
            Root.Transform.Position = (20,30);
            Root.Transform.Scale = (50,70);
        }

        public override void Update()
        {
            Root.Transform.X += 1f;
        }
    }

    class Player : Root
    {
        public Player()
        {
            AddComponent<Quad>();
            AddComponent<TestScript>();
        }
    }

    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }

        public Player Player;

        public override void Ready()
        {
            Player = new Player();
        }
    }

    class Program
    {
        static void Main()
        {
            Game game = new(512, "Hello");
        }
    }
}