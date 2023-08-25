using BloodlineEngine;

namespace Sandbox
{
    class Ground : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col("757575")
                .Pos((0f, 400f))
                .Scl((512f, 15f));
            CreateComponent<BoxCollider>()
                .Pin("Ground");
        }
    }

    class PlayerMovement : Component
    {
        public override void Update()
        {
            PhysicsBody physicsBody = Root.GetComponent<PhysicsBody>();
            if (Input.IsKeyPressed(Keys.W)) physicsBody.Velocity.Y = -10f;
            if (Input.IsKeyPressed(Keys.A)) physicsBody.Velocity.X = -7f;
            if (Input.IsKeyPressed(Keys.D)) physicsBody.Velocity.X = 7f;
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Bmp(new Bitmap("Assets/test.png"))
                .Ctr(256f)
                .Scl(60f);
            CreateComponent<BoxCollider>();
            CreateComponent<PhysicsBody>()
                .Gra(1f)
                .Dra(1f)
                .Avp(() =>
                {
                    if (!GetComponent<BoxCollider>().IsColliding("Ground")) return true;
                    GetComponent<PhysicsBody>().Velocity.Y = -GetComponent<PhysicsBody>().Velocity.Y / 2f;
                    return false;
                });
            CreateComponent<PlayerMovement>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        Player Player;
        List<object> Environment;

        public override void Ready()
        {
            Renderer.ClearColor = "000000";

            Player = new Player();

            Environment = new List<object>()
            { new Ground() };
        }

        public override void Update()
        {
            Debug.Trace(Time.FPS);
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
