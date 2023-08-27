using BloodlineEngine;

namespace Sandbox
{
    class Ground : Root
    {
        public override void Init()
        {
            CreateComponent<Quad>()
                .Col("757575")
                .Scl((5120f, 15f))
                .Ctr((256f, 400f));
            CreateComponent<BoxCollider>()
                .Pin("Ground");
        }
    }

    class PlayerBulletController : Component
    {
        bool ReachedMouse = false;

        public override void Update()
        {
            PhysicsBody body = Root.GetComponent<PhysicsBody>();
            if (body == null) return;
            if (!ReachedMouse) body.Velocity += Vector2.MoveTowards(Transform.Center, Input.MousePositionWorld, 1f) - Transform.Center;
            if (Vector2.GetDistance(Transform.Center, Input.MousePositionWorld) < 35f)
            {
                ReachedMouse = true;
                body.Drag = 0.1f;
            }
            if (body.Velocity.X == 0f && body.Velocity.Y == 0f) Root.Disable();
        }
    }

    class FriendlyBullet : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Bmp(new Bitmap("Assets/friendly_bullet.png"))
                .Ctr(Await<Vector2>("Player"))
                .Scl(20f)
                .Zee(50);
            CreateComponent<PhysicsBody>()
                .Dra(new Vector2(0.6f));
            CreateComponent<PlayerBulletController>();
        }
    }

    class PlayerMovement : Component
    {
        public override void Update()
        {
            PhysicsBody physicsBody = Root.GetComponent<PhysicsBody>();
            Ground ground = Await<Ground>("Ground");
            Raycast groundCheck = new(Transform.BottomLeft, Transform.BottomLeft + 17f);
            if (Input.IsKeyPressed(Keys.W) && groundCheck.IsColliding(ground.Transform.Vertices)) physicsBody.Velocity.Y = -21f;
            if (Input.IsKeyPressed(Keys.A)) physicsBody.Velocity.X = -7f;
            if (Input.IsKeyPressed(Keys.D)) physicsBody.Velocity.X = 7f;
        }
    }

    class CameraFollow : Component
    {
        public override void FixedUpdate()
        {
            Camera camera = Await<Camera>("Camera");
            if (camera != null) camera.Center = Vector2.GetDistance(camera.Center, Transform.Center) < 200f ?
                    Vector2.MoveTowards(camera.Center, Transform.Center + (0, -40f), 5.5f) :
                    Vector2.MoveTowards(camera.Center, Transform.Center + (0, -40f), 7f);
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Bmp(new Bitmap("Assets/player.png"))
                .Ctr(256f)
                .Scl(80f);
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
            CreateComponent<PlayerMovement>()
                .Pass("Ground", Await<Ground>("Ground"));
            CreateComponent<CameraFollow>()
                .Pass("Camera", Await<Camera>("Camera"));
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        bool m_SemiAuto = true;

        public override void Ready()
        {
            Renderer.ClearColor = "000000";

            Ground ground = Instantiate<Ground>();
            Instantiate<Player>()
                .Pass("Ground", ground)
                .Pass("Camera", RenderedCamera);
        }

        public override void Update()
        {
            if (Input.IsMouseButtonPressed(MouseButtons.Left) && m_SemiAuto == true)
            {
                Player player = GetInstance<Player>();
                Vector2 position = player.Transform.Center + (-30f, 0f);
                ThrowawayInstance<FriendlyBullet>()
                    .Pass("Player", position);
                m_SemiAuto = false;
            }

            if (!Input.IsMouseButtonPressed(MouseButtons.Left) && m_SemiAuto == false) m_SemiAuto = true;
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
