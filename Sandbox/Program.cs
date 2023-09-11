using BloodlineEngine;

namespace Sandbox
{
    class Game : BLApplication
    {
        public Game() : base(512f, "Minigame", lauchWithConsoleShown: true) { }

        public override void Ready()
        {
            Renderer.ClearColor = "#000000";

            Input.KeyDown += (key) => Debug.Trace(key);
            Input.ButtonDown += (button) => Debug.Trace(button);
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
