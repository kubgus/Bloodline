using BloodlineEngine;
using System.Windows.Forms;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public Vector2 Velocity = Vector2.Zero();
        public float Drag = 0.35f;
        public float Gravity = 1f;
        BoxCollider Collider = new();

        public override void Spark()
        {
            Collider = Root.GetComponent<BoxCollider>();
        }

        public override void Update()
        {
            float speed = Await<float>("Speed");
            if (Input.IsKeyPressed(Keys.A)) Velocity.X = -speed;
            if (Input.IsKeyPressed(Keys.D)) Velocity.X = speed;

        }

        public override void FixedUpdate()
        {
            if (Velocity.X > 0) Velocity.X = Velocity.X - Drag > 0 ? Velocity.X - Drag : 0;
            if (Velocity.X < 0) Velocity.X = Velocity.X + Drag < 0 ? Velocity.X + Drag : 0;
            MoveUnlessColliding((Velocity.X, 0), Collider);
        }

        bool MoveUnlessColliding(Vector2 direction, BoxCollider collider, string tag = "Ground")
        {
            bool collided = false;
            Transform.Position += direction;
            while (collider.IsColliding(tag))
            {
                Transform.Position -= direction * 0.1f;
                collided = true;
            }
            return collided;
        }
    }

    class PlayerCollider : BoxCollider { }

    class Obstacle : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col(Await<Color4>("Color") ?? 0)
                .Pos(Await<Vector2>("Position") ?? 0)
                .Scl(Await<Vector2>("Scale") ?? 1f);
            CreateComponent<BoxCollider>()
                .Pin("Ground");
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col((202, 100, 50))
                .Pos(100)
                .Scl(50);
            CreateComponent<PlayerCollider>()
                .Pin("Player");
            CreateComponent<PlayerMovement>()
                .Pass("Speed", Await("Speed"));
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        float PlayerSpeed = 5f;

        Player Player;

        public override void Ready()
        {
            Renderer.ClearColor = Color4.FromHex("000000");

            Player = (Player)new Player()
                .Pass("Speed", PlayerSpeed);

            Obstacle g = (Obstacle)new Obstacle()
                .Pass<Vector2>("Position", (0f, 300f))
                .Pass<Vector2>("Scale", (500f, 10f))
                .Pass<Color4>("Color", (255, 10, 10));
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
