using BloodlineEngine;
using System.Windows.Forms;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public override void Update()
        {
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

        private float Speed = 10f;

        public override void Ready()
        {
            Player = new Character();

            Renderer.ClearColor = (100, 100, 255);
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(Keys.W)) { RenderedCamera.MoveRotated((0f, -Speed)); }
            if (Input.IsKeyPressed(Keys.S)) { RenderedCamera.MoveRotated((0f, Speed)); }
            if (Input.IsKeyPressed(Keys.A)) { RenderedCamera.MoveRotated((-Speed, 0f)); }
            if (Input.IsKeyPressed(Keys.D)) { RenderedCamera.MoveRotated((Speed, 0f)); }
            if (Input.IsKeyPressed(Keys.Right)) { RenderedCamera.Rotation -= Speed / 5f; }
            if (Input.IsKeyPressed(Keys.Left)) { RenderedCamera.Rotation += Speed / 5f; }
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
