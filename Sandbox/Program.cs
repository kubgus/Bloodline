using BloodlineEngine;

namespace Sandbox
{
    class EnemyBehavior : Component
    {
        int Direction = Chance.Pick(new int[] {-1, 1});

        public override void Update()
        {
            float speed = (float)Args.UseOr("Speed", 1f);

            Transform.Position.Y += speed;
            Transform.Position.X += Direction * speed;

            if (Transform.Position.X < -240f || Transform.Position.X > 240f) Direction *= -1;

            if (Root.GetComponent<BoxCollider>().IsColliding("Bullet"))
                Root.Disable();

            if (Transform.Position.Y > 256)
                Game.WinState = -1;
        }
    }

    class Enemy : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Src("assets/invader.png")
                .Hof(Chance.Pick(new bool[] {true, false}))
                .Pos((Chance.RandomFloat(-240f, 240f), -250f))
                .Scl(40f);
            CreateComponent<BoxCollider>()
                .Pin("Enemy");

            CreateComponent<EnemyBehavior>();
        }
    }

    class BulletBehavior : Component
    {
        public override void Update()
        {
            Transform.Position.Y -= (float)Args.UseOr("Speed", 5f);

            if (Transform.Position.Y < -300f)
                Root.Disable();
        }
    }

    class Bullet : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col(Color4.Red)
                .Pos((Vector2)Args.UseOr("Position", 0f))
                .Scl((5f, 25f));
            CreateComponent<BoxCollider>()
                .Pin("Bullet");

            CreateComponent<BulletBehavior>();
        }
    }

    class KeyboardMovement : Component
    {
        public override void Update()
        {
            float fallbackSpeed = 1f;
            if (Input.IsKeyPressed(Keyboard.W)) Transform.Position.Y -= (float)Args.UseOr("VerticalSpeed", fallbackSpeed);
            if (Input.IsKeyPressed(Keyboard.S)) Transform.Position.Y += (float)Args.UseOr("VerticalSpeed", fallbackSpeed);
            if (Input.IsKeyPressed(Keyboard.A)) Transform.Position.X -= (float)Args.UseOr("HorizontalSpeed", fallbackSpeed);
            if (Input.IsKeyPressed(Keyboard.D)) Transform.Position.X += (float)Args.UseOr("HorizontalSpeed", fallbackSpeed);
        }
    }

    class Die : Component
    {
        public override void Update()
        {
            if (Root.GetComponent<BoxCollider>().IsColliding("Enemy"))
                Game.WinState = -1;
        }
    }

    class Rocket : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Src("assets/rocket.png")
                .Pos((0f, 128f))
                .Scl(50f)
                .Zee(10f);
            CreateComponent<BoxCollider>();

            CreateComponent<KeyboardMovement>();
            CreateComponent<Die>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Space Invaders?", lauchWithConsoleShown: false) { }

        public static int WinState = 0;

        float EnemySpeed = 0.3f;

        public override void Ready()
        {
            Renderer.ClearColor = "#000000";

            Instantiate<Rocket>()
                .Arg("VerticalSpeed", 1f)
                .Arg("HorizontalSpeed", 2.5f);

            Input.KeyDown += (key) =>
            {
                if (key == Keyboard.Space)
                {
                    ThrowawayInstance<Bullet>()
                        .Arg("Position", GetInstance<Rocket>().Transform.Position.Copy);
                }
            };
        }

        public override void FixedUpdate()
        {
            if (Time.ElapsedFrames % 50 == 0)
            {
                ThrowawayInstance<Enemy>()
                    .Arg("Speed", EnemySpeed);
                EnemySpeed += 0.015f;
            }
        }

        public override void Update()
        {
            if (WinState == -1)
            {
                GetInstance<Rocket>().Disable();
            }
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
