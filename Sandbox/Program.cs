using BloodlineEngine;

namespace Sandbox
{
    class CollisionColor : Component
    {
        public override void Update()
        {
            if (Root.GetComponent<BoxCollider>().IsColliding("Player"))
                Root.GetComponent<Quad>().Color = "0000ff";
            else
                Root.GetComponent<Quad>().Color = "00ff00";
        }
    }

    class TestObject : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col("00ff00")
                .Scl(50f)
                .Rot(Chance.RandomFloat(0f, 360f))
                .Zee(-5f);
            CreateComponent<BoxCollider>();
            CreateComponent<CollisionColor>();
        }
    }

    class KeyboardMovement : Component
    {
        public override void Update()
        {
            if (Input.IsKeyPressed(Keyboard.W)) { Transform.Position.Y -= 2f; }
            if (Input.IsKeyPressed(Keyboard.S)) { Transform.Position.Y += 2f; }
            if (Input.IsKeyPressed(Keyboard.A)) { Transform.Position.X -= 2f; }
            if (Input.IsKeyPressed(Keyboard.D)) { Transform.Position.X += 2f; }

            if (Input.IsKeyPressed(Keyboard.Q)) { Transform.Scale -= 1f; }
            if (Input.IsKeyPressed(Keyboard.E)) { Transform.Scale += 1f; }
            if (Input.IsKeyPressed(Keyboard.Z)) { Transform.Rotation -= 1f; }
            if (Input.IsKeyPressed(Keyboard.X)) { Transform.Rotation += 1f; }
        }
    }

    class CameraController : Component
    {
        public override void Update()
        {
            Camera camera = (Camera)Args["Camera"];
            if (Input.IsKeyPressed(Keyboard.Up)) { camera.Position.Y -= 2f; }
            if (Input.IsKeyPressed(Keyboard.Down)) { camera.Position.Y += 2f; }
            if (Input.IsKeyPressed(Keyboard.Left)) { camera.Position.X -= 2f; }
            if (Input.IsKeyPressed(Keyboard.Right)) { camera.Position.X += 2f; }

            if (Input.IsKeyPressed(Keyboard.M)) { camera.Scale -= 0.01f; }
            if (Input.IsKeyPressed(Keyboard.N)) { camera.Scale += 0.01f; }
            if (Input.IsKeyPressed(Keyboard.B)) { camera.Rotation -= 0.1f; }
            if (Input.IsKeyPressed(Keyboard.V)) { camera.Rotation += 0.1f; }
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                //.Src("assets/player.png")
                .Col(Color4.Red)
                .Scl(256f)
                .Pos(0f);
            CreateComponent<KeyboardMovement>();
            CreateComponent<CameraController>();
            CreateComponent<BoxCollider>()
                .Pin("Player");
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

            ThrowawayInstance<TestObject>().Transform.Position = (160f, 160f);
            ThrowawayInstance<TestObject>().Transform.Position = (1f, -80f);
        }

        public override void Update()
        {
            Debug.Trace(GetInstance<Player>().Transform.Position);
            Debug.Trace(RenderedCamera.Position);
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
