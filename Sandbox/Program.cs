using BloodlineEngine;
using Microsoft.VisualBasic.Logging;

namespace Sandbox
{
    public class TestScript : Component
    {
        public override void Ready()
        {
            Root.Transform.X = 50;
            Root.Transform.Y = 50;
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