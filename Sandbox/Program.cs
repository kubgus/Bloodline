using BloodlineEngine;
using System.Windows.Forms;

namespace Sandbox
{
    class PlayerMovement : Component
    {
        public override void Update()
        {
            float speed = (float)this["Speed"];
            if (Input.IsKeyPressed(Keys.W)) { Transform.Position.Y -= speed; }
            if (Input.IsKeyPressed(Keys.S)) { Transform.Position.Y += speed; }
            if (Input.IsKeyPressed(Keys.A)) { Transform.Position.X -= speed; }
            if (Input.IsKeyPressed(Keys.D)) { Transform.Position.X += speed; }
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
            CreateComponent<PlayerMovement>()
                .Arg("Speed", Await("Speed"));
        }
    }

    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        float PlayerSpeed = 5f;

        Player Player;

        public override void Ready()
        {
            Player = (Player)new Player()
                .Pass("Speed", PlayerSpeed);
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
