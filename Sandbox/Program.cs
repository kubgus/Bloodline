using BloodlineEngine;

namespace Sandbox
{
    class KeyboardMovement : Component
    {
        float m_Dir = 0f;

        public override void Update()
        {
            float speed = (float)Args["Speed"];

            if (Input.IsKeyPressed(Keys.W))
            {
                if (Input.IsKeyPressed (Keys.D)) { m_Dir = -45f; }
                else if (Input.IsKeyPressed (Keys.A)) { m_Dir = -135f; }
                else { m_Dir = -90f; }
            } else if (Input.IsKeyPressed (Keys.S))
            {
                if (Input.IsKeyPressed(Keys.D)) { m_Dir = 45f; }
                else if (Input.IsKeyPressed(Keys.A)) { m_Dir = 135f; }
                else { m_Dir = 90f; }
            } else if (Input.IsKeyPressed(Keys.D)) { m_Dir = 0f; }
            else if (Input.IsKeyPressed(Keys.A)) { m_Dir = 180f; }
            else { speed = 0f; }

            Transform.Position = Vector2.MoveInDirection(Transform.Position, m_Dir, speed);
        }
    }

    class CameraFollow : Component
    {
        Camera? m_Camera;

        public override void Awake()
        {
            m_Camera = (Camera)Args["Camera"];
            m_Camera.Center = Transform.Center;
        }

        public override void Update()
        {
            MoveCamera();   
        }

        private void MoveCamera()
        {
            if (m_Camera is null) return;
            float speed = (float)Args["Speed"] - 0.5f;
            if (Vector2.GetDistance(m_Camera.Center, Transform.Center) > 50f) { speed = (float)Args["Speed"]; }
            m_Camera.Center = Vector2.MoveTowards(m_Camera.Center, Transform.Center, speed);
        }
    }

    class Player : Root
    {
        public override void Init()
        {
            CreateComponent<Sprite>()
                .Bmp(new Bitmap("Assets/player.png"))
                .Scl(64f);
            CreateComponent<KeyboardMovement>();
            CreateComponent<CameraFollow>();
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public override void Ready()
        {
            Renderer.ClearColor = Color.Black;

            Instantiate<Player>()
                .Arg("Speed", 5f)
                .Arg("Camera", RenderedCamera);
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
