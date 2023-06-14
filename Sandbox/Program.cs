using BloodlineEngine;

namespace Sandbox
{
    class Whatever : Component
    {
        public override void Draw()
        {
            if (Root.GetComponent<BoxCollider>().IsColliding("Player"))
            {
                Root.GetComponent<Quad>().Color = (255, 0, 33);
            }
            else
            {
                Root.GetComponent<Quad>().Color = (255, 100, 255);
            }

            if (Input.IsMouseButtonPressed(System.Windows.Forms.MouseButtons.Left)) { Disable(); }
        }
    }

    class Box : Root
    {
        public Box()
        {
            Transform.Position = 300f;
            Transform.Rotation = 50f;
            Transform.Scale = 50f;

            AddComponent<Quad>();
            AddComponent<BoxCollider>().Tag = "Wall";
            AddComponent<Whatever>();
        }
    }

    class PlayerMovement : Component
    {
        public override void FixedUpdate()
        {
            PlayerData playerData = Root.GetComponent<PlayerData>();

            Vector2 direction = 0f;

            if (Input.IsKeyPressed(System.Windows.Forms.Keys.W)) { direction.Y = -1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.S)) { direction.Y = 1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.A)) { direction.X = -1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.D)) { direction.X = 1f; }

            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Z)) { Transform.Z = 1.7f; }
            else { Transform.Z = 0f; }

            Vector2 move = direction * playerData.Speed * Time.DeltaTime;
            Transform.Position.X += move.X;
            Transform.Position.Y += move.Y;
            if (direction.X > 0f) { Transform.Rotation++; }
            if (direction.X < 0f) { Transform.Rotation--; }
        }
    }

    class PlayerData : Component
    {
        public float Speed;
    }

    class ColorScript : Component
    {
        public override void Update()
        {
            Root.GetComponent<Quad>().Color.R = (int)Transform.X / (512 / 255);
            Root.GetComponent<Quad>().Color.G = (int)(512 - Transform.X) / (512 / 255);
            Root.GetComponent<Quad>().Color.B = (int)Transform.Y / (512 / 255);
        }
    }

    class Player : Root
    {
        public Player()
        {
            Transform.Position = 30f;
            Transform.Scale = 30f;

            AddComponent<Quad>().Color = (255, 0, 0);

            AddComponent<PlayerData>().Speed = 50f;
            AddComponent<PlayerMovement>();
            AddComponent<ColorScript>();
            AddComponent<BoxCollider>().Tag = "Player";
        }
    }

    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }

        public Player? Player;
        public Box? Box;

        public override void Ready()
        {
            Player = new Player();
            Box = new Box();
        }
    }

    class Program
    {
        static void Main()
        {
            Game game = new(512, "Hello");
        }
    }
}