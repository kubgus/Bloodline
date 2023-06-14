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

        List<Dot> dots;

        public override void Ready()
        {
            Window.Renderer.ClearColor = 0;

            dots = new List<Dot>();
            for (int i = 0; i < 5000; i++)
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
    }

    class Program
    {
        static void Main()
        {
            Game game = new(512, "Hello");
        }
    }
}