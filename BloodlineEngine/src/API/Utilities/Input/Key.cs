namespace BloodlineEngine
{
    public class Key
    {
        public Keyboard Keycode { get; set; }

        public Key(Keyboard keycode)
        {
            Keycode = keycode;
        }

        public override string ToString() => $"{Keycode}";

        public static implicit operator Keyboard(Key key)
        { return key.Keycode; }
        public static implicit operator Key(Keyboard keyboard)
        { return new Key(keyboard); }

        public static implicit operator SDL.SDL_Keycode(Key key)
        { return m_Map.Find((x) => x.Item1 == key).Item2; }
        public static implicit operator Key(SDL.SDL_Keycode keycode)
        { return m_Map.Find((x) => x.Item2 == keycode).Item1; }

        private static readonly List<(Keyboard, SDL.SDL_Keycode)> m_Map = new()
        {
            (Keyboard.None, SDL.SDL_Keycode.SDLK_UNKNOWN),
            (Keyboard.A, SDL.SDL_Keycode.SDLK_a),
            (Keyboard.B, SDL.SDL_Keycode.SDLK_b),
            (Keyboard.C, SDL.SDL_Keycode.SDLK_c),
            (Keyboard.D, SDL.SDL_Keycode.SDLK_d),
            (Keyboard.E, SDL.SDL_Keycode.SDLK_e),
            (Keyboard.F, SDL.SDL_Keycode.SDLK_f),
            (Keyboard.G, SDL.SDL_Keycode.SDLK_g),
            (Keyboard.H, SDL.SDL_Keycode.SDLK_h),
            (Keyboard.I, SDL.SDL_Keycode.SDLK_i),
            (Keyboard.J, SDL.SDL_Keycode.SDLK_j),
            (Keyboard.K, SDL.SDL_Keycode.SDLK_k),
            (Keyboard.L, SDL.SDL_Keycode.SDLK_l),
            (Keyboard.M, SDL.SDL_Keycode.SDLK_m),
            (Keyboard.N, SDL.SDL_Keycode.SDLK_n),
            (Keyboard.O, SDL.SDL_Keycode.SDLK_o),
            (Keyboard.P, SDL.SDL_Keycode.SDLK_p),
            (Keyboard.Q, SDL.SDL_Keycode.SDLK_q),
            (Keyboard.R, SDL.SDL_Keycode.SDLK_r),
            (Keyboard.S, SDL.SDL_Keycode.SDLK_s),
            (Keyboard.T, SDL.SDL_Keycode.SDLK_t),
            (Keyboard.U, SDL.SDL_Keycode.SDLK_u),
            (Keyboard.V, SDL.SDL_Keycode.SDLK_v),
            (Keyboard.W, SDL.SDL_Keycode.SDLK_w),
            (Keyboard.X, SDL.SDL_Keycode.SDLK_x),
            (Keyboard.Y, SDL.SDL_Keycode.SDLK_y),
            (Keyboard.Z, SDL.SDL_Keycode.SDLK_z),
            (Keyboard.Num0, SDL.SDL_Keycode.SDLK_0),
            (Keyboard.Num1, SDL.SDL_Keycode.SDLK_1),
            (Keyboard.Num2, SDL.SDL_Keycode.SDLK_2),
            (Keyboard.Num3, SDL.SDL_Keycode.SDLK_3),
            (Keyboard.Num4, SDL.SDL_Keycode.SDLK_4),
            (Keyboard.Num5, SDL.SDL_Keycode.SDLK_5),
            (Keyboard.Num6, SDL.SDL_Keycode.SDLK_6),
            (Keyboard.Num7, SDL.SDL_Keycode.SDLK_7),
            (Keyboard.Num8, SDL.SDL_Keycode.SDLK_8),
            (Keyboard.Num9, SDL.SDL_Keycode.SDLK_9),
            (Keyboard.Escape, SDL.SDL_Keycode.SDLK_ESCAPE),
            (Keyboard.F1, SDL.SDL_Keycode.SDLK_F1),
            (Keyboard.F2, SDL.SDL_Keycode.SDLK_F2),
            (Keyboard.F3, SDL.SDL_Keycode.SDLK_F3),
            (Keyboard.F4, SDL.SDL_Keycode.SDLK_F4),
            (Keyboard.F5, SDL.SDL_Keycode.SDLK_F5),
            (Keyboard.F6, SDL.SDL_Keycode.SDLK_F6),
            (Keyboard.F7, SDL.SDL_Keycode.SDLK_F7),
            (Keyboard.F8, SDL.SDL_Keycode.SDLK_F8),
            (Keyboard.F9, SDL.SDL_Keycode.SDLK_F9),
            (Keyboard.F10, SDL.SDL_Keycode.SDLK_F10),
            (Keyboard.F11, SDL.SDL_Keycode.SDLK_F11),
            (Keyboard.F12, SDL.SDL_Keycode.SDLK_F12),
            (Keyboard.PrintScreen, SDL.SDL_Keycode.SDLK_PRINTSCREEN),
            (Keyboard.ScrollLock, SDL.SDL_Keycode.SDLK_SCROLLLOCK),
            (Keyboard.Pause, SDL.SDL_Keycode.SDLK_PAUSE),
            (Keyboard.Insert, SDL.SDL_Keycode.SDLK_INSERT),
            (Keyboard.Home, SDL.SDL_Keycode.SDLK_HOME),
            (Keyboard.PageUp, SDL.SDL_Keycode.SDLK_PAGEUP),
            (Keyboard.Delete, SDL.SDL_Keycode.SDLK_DELETE),
            (Keyboard.End, SDL.SDL_Keycode.SDLK_END),
            (Keyboard.PageDown, SDL.SDL_Keycode.SDLK_PAGEDOWN),
            (Keyboard.Right, SDL.SDL_Keycode.SDLK_RIGHT),
            (Keyboard.Left, SDL.SDL_Keycode.SDLK_LEFT),
            (Keyboard.Down, SDL.SDL_Keycode.SDLK_DOWN),
            (Keyboard.Up, SDL.SDL_Keycode.SDLK_UP),
            (Keyboard.Backspace, SDL.SDL_Keycode.SDLK_BACKSPACE),
            (Keyboard.Tab, SDL.SDL_Keycode.SDLK_TAB),
            (Keyboard.CapsLock, SDL.SDL_Keycode.SDLK_CAPSLOCK),
            (Keyboard.Enter, SDL.SDL_Keycode.SDLK_RETURN),
            (Keyboard.Shift, SDL.SDL_Keycode.SDLK_LSHIFT),
            (Keyboard.Control, SDL.SDL_Keycode.SDLK_LCTRL),
            (Keyboard.Alt, SDL.SDL_Keycode.SDLK_LALT),
            (Keyboard.Space, SDL.SDL_Keycode.SDLK_SPACE),
            (Keyboard.SuperKey, SDL.SDL_Keycode.SDLK_LGUI),
            (Keyboard.ContextMenu, SDL.SDL_Keycode.SDLK_APPLICATION)
        };
    }

    public enum Keyboard
    {
        None = -1,
        A, B, C, D, E, F, G, H, I, J, K, L, M,
        N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
        Escape, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
        PrintScreen, ScrollLock, Pause, Insert, Home, PageUp,
        Delete, End, PageDown, Right, Left, Down, Up, Backspace,
        Tab, CapsLock, Enter, Shift, Control, Alt, Space, SuperKey, ContextMenu
    }
}
