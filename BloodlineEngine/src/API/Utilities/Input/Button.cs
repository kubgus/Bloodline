using SDL2;

namespace BloodlineEngine
{
    /// <summary>
    /// Represents a single mouse button.
    /// </summary>
    public class Button
    {
        Mouse Mousebutton { get; set; }

        public Button(Mouse mousebutton)
        {
            Mousebutton = mousebutton;
        }

        public static implicit operator Mouse(Button button)
        { return button.Mousebutton; }
        public static implicit operator Button(Mouse mouse)
        { return new Button(mouse); }

        public static implicit operator uint(Button button)
        { return m_Map.Find((x) => x.Item1 == button).Item2; }
        public static implicit operator Button(uint button)
        { return m_Map.Find((x) => x.Item2 == button).Item1; }

        private static readonly List<(Mouse, uint)> m_Map = new()
        {
            (Mouse.Left, SDL.SDL_BUTTON_LEFT),
            (Mouse.Middle,SDL.SDL_BUTTON_MIDDLE),
            (Mouse.Right, SDL.SDL_BUTTON_RIGHT),
            (Mouse.X1, SDL.SDL_BUTTON_X1 ),
            (Mouse.X2, SDL.SDL_BUTTON_X2 ),
        };
    }

    public enum Mouse
    {
        None = -1,
        Left, Middle, Right,
        X1, X2
    }
}
