using BloodlineEngine;

namespace Sandbox
{
    class KeyboardMovement : Component
    {
        public override void Update()
        {
            if (Input.IsKeyPressed(Keyboard.W)) { Transform.Position.Y -= 2f; }
            if (Input.IsKeyPressed(Keyboard.S)) { Transform.Position.Y += 2f; }
            if (Input.IsKeyPressed(Keyboard.A)) { Transform.Position.X -= 2f; }
            if (Input.IsKeyPressed(Keyboard.D)) { Transform.Position.X += 2f; }
        }
    }

    class CameraFollow : Component
    {
        public override void Update()
        {
            Camera camera = (Camera)Args["Camera"];
            if (Input.IsKeyPressed(Keyboard.Up)) { camera.Position.Y -= 2f; }
            if (Input.IsKeyPressed(Keyboard.Down)) { camera.Position.Y += 2f; }
            if (Input.IsKeyPressed(Keyboard.Left)) { camera.Position.X -= 2f; }
            if (Input.IsKeyPressed(Keyboard.Right)) { camera.Position.X += 2f; }
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Src("assets/player.png")
                //.Col(Color4.Red)
                .Scl(256f)
                .Ctr(256f);
            CreateComponent<KeyboardMovement>();
            CreateComponent<CameraFollow>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public override void Ready()
        {
            Renderer.ClearColor = "#000000";

            Instantiate<Player>()
                .Arg("Camera", RenderedCamera);
        }

        public override void Update()
        {
            
            // Debug.Trace(RenderedCamera.Position);
        }
    }

    class Program
    {
        static void Main()
        {
            Bloodline.StartApplication<Game>();
        }
    }
}
