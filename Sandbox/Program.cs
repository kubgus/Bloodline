using BloodlineEngine;
using System.Windows.Forms;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public override void Update()
        {
            if (Input.IsKeyPressed(Keys.W)) { Transform.Y -= 5f; }
            if (Input.IsKeyPressed(Keys.S)) { Transform.Y += 5f; }
            if (Input.IsKeyPressed(Keys.A)) { Transform.X -= 5f; }
            if (Input.IsKeyPressed(Keys.D)) { Transform.X += 5f; }
        }
    }

    class Character : Root
    {
        public Character()
        {
            Transform.Scale = 30f;
            Transform.Center = 256f;

            AddComponent<Quad>().Color = (255, 255, 100);
            AddComponent<PlayerMovement>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public Character? Player;

        public override void Ready()
        {
            Player = new Character();

            Renderer.ClearColor = (100, 100, 255);
        }

        public override void Update()
        {
            RendererCamera.MoveTo(Player.Transform.Center - 256f);
        }
    }

    class Program
    {
        static void Main()
        {
            Game game = new();
        }
    }
}
