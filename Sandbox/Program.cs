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
            Root.GetComponent<Quad>().Color = (44, 20, 40);
        }

        public override void Update()
        {
            Root.Transform.X += 1f;
            Root.GetComponent<Quad>().Color.R += 1;

            if (Root.Transform.X > 300f) { Root.Transform.BottomLeft = (0, 300); }
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

    class ObM : Component
    {
        public override void Ready()
        {
            Root.Transform.Position = (10, 300);
            Root.Transform.Scale = (10, 30);
            Root.GetComponent<Quad>().Color = (0, 255, 0, 255);
        }
    }

    class Obstelek : Root
    {
        public Obstelek()
        {
            AddComponent<Quad>();
            AddComponent <ObM>();
        }
    }

    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }

        public Player Player;
        public Obstelek Obstelek;

        public override void Ready()
        {
            Player = new Player();
            Obstelek = new Obstelek();
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