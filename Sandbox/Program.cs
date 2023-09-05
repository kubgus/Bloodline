using BloodlineEngine;

namespace Sandbox
{
    class KeyboardMovement : Component
    {
        public override void Update()
        {
            if (Input.IsKeyPressed(Keyboard.W)) { Transform.Position.Y -= 5f; }
            if (Input.IsKeyPressed(Keyboard.S)) { Transform.Position.Y += 5f; }
            if (Input.IsKeyPressed(Keyboard.A)) { Transform.Position.X -= 5f; }
            if (Input.IsKeyPressed(Keyboard.D)) { Transform.Position.X += 5f; }
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col(Color4.Red)
                .Pos(0f)
                .Scl(30f);
            CreateComponent<KeyboardMovement>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public override void Ready()
        {
            Renderer.ClearColor = "#000000";

            Instantiate<Player>();
        }

        public override void Update()
        {
            // Debug.Trace(Time.ElapsedFrames);
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
