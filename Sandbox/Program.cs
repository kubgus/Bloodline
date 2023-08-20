using BloodlineEngine;
using System.Windows.Forms;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public override void Update()
        {
            float speed = 10f;
            Camera renderedCamera = (Camera)this["Camera"];
            if (Input.IsKeyPressed(Keys.W)) { renderedCamera.MoveRotated((0f, -speed)); }
            if (Input.IsKeyPressed(Keys.S)) { renderedCamera.MoveRotated((0f, speed)); }
            if (Input.IsKeyPressed(Keys.A)) { renderedCamera.MoveRotated((-speed, 0f)); }
            if (Input.IsKeyPressed(Keys.D)) { renderedCamera.MoveRotated((speed, 0f)); }
            if (Input.IsKeyPressed(Keys.Right)) { renderedCamera.Rotation -= speed / 5f; }
            if (Input.IsKeyPressed(Keys.Left)) { renderedCamera.Rotation += speed / 5f; }
        }
    }

    class Character : Root
    {
        public Character()
        {
            Transform.Scale = 30f;
            Transform.Center = 256f;

            CreateComponent<Quad>().Color = (255, 255, 100);
            CreateComponent<PlayerMovement>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public Character? Player;

        public override void Ready()
        {
            Renderer.ClearColor = (100, 100, 255);

            Player = new Character();
            Player.GetComponent<PlayerMovement>()["Camera"] = RenderedCamera;
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
