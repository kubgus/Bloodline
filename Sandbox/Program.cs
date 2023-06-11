using BloodlineEngine;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public override void Update()
        {
            PlayerData playerData = Root.GetComponent<PlayerData>();

            Vector2 direction = 0f;

            if (Input.IsKeyPressed(System.Windows.Forms.Keys.W)) { direction.Y = -1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.S)) { direction.Y = 1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.A)) { direction.X = -1f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.D)) { direction.X = 1f; }

            Root.Transform.Position += direction * playerData.Speed * Time.DeltaTime;
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
            Root.GetComponent<Quad>().Color.R = (int)Root.Transform.X / (512 / 255);
            Root.GetComponent<Quad>().Color.G = (int)(512 - Root.Transform.X) / (512 / 255);
            Root.GetComponent<Quad>().Color.B = (int)Root.Transform.Y / (512 / 255);
        }
    }

    class Player : Root
    {
        public Player()
        {
            Transform.Position = 30f;
            Transform.Scale = 10f;

            AddComponent<Quad>().Color = (255,0,0);

            AddComponent<PlayerData>().Speed = 50f;
            AddComponent<PlayerMovement>();
            AddComponent<ColorScript>();
        }
    }

    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }

        public Player Player;

        public override void Ready()
        {
            Player = new Player();
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