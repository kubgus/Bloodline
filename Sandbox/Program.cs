using BloodlineEngine;

namespace Sandbox
{
    class Dot : Root
    {
        public Dot()
        {
            Transform.Scale = 4f;

            AddComponent<Quad>().Color = (255, 20, 20);
        }
    }

    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }

        List<Dot> dots = new();
        float speed = 10f;

        public override void Ready()
        {
            Renderer.ClearColor = 0;

            for (int i = 0; i < 256; i++)
            {
                Dot dot = new();
                dots.Add(dot);
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < dots.Count; i++)
            {
                Dot dot = dots[i];
                float time = Time.ElapsedMilliseconds / 500000f;
                dot.Transform.Center = (i * (512f / dots.Count), MathF.Sin(time * i) * 100f + 256f);
            }
        }

        public override void Update()
        {
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.W)) { RenderedCamera.Transform.Position.Y -= speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.S)) { RenderedCamera.Transform.Position.Y += speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.A)) { RenderedCamera.Transform.Position.X -= speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.D)) { RenderedCamera.Transform.Position.X += speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Up)) { RenderedCamera.Transform.Scale += speed / 500f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Down)) { RenderedCamera.Transform.Scale -= speed / 500f; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Right)) { RenderedCamera.Transform.Rotation += speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Left)) { RenderedCamera.Transform.Rotation -= speed; }
            if (Input.IsKeyPressed(System.Windows.Forms.Keys.C)) { BLConsoleManager.CloseConsole(); }
            else { BLConsoleManager.OpenConsole(); }

            //if (Time.ElapsedFrames % 100 == 0) { BLConsoleManager.ToggleConsole(); }

            if (Input.IsKeyPressed(System.Windows.Forms.Keys.Z))
            { Debug.Error(Time.ElapsedMilliseconds); } // Just for fun
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
