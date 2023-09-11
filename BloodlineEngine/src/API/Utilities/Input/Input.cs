namespace BloodlineEngine
{
    public static class Input
    {
        public static bool Scrolling { get; private set; }
        public static Vector2 MousePosition { get; private set; } = new();
        public static Vector2 MousePositionWindow => MousePosition;
        public static Vector2 MousePositionWorld => MousePosition + m_CameraPosition;

        private static Vector2 m_WindowSize = new();
        private static Vector2 m_CameraPosition = new();

        public static event Action<Keyboard>? KeyDown;
        public static event Action<Keyboard>? KeyUp;
        private readonly static HashSet<Keyboard> m_ActiveKeys = new();

        public static event Action<Button>? ButtonDown;
        public static event Action<Button>? ButtonUp;
        private readonly static HashSet<Button> m_ActiveButtons = new();

        public static void Press(Key key) { KeyDown?.Invoke(key); m_ActiveKeys.Add(key); }
        public static void Release(Key key) { KeyUp?.Invoke(key); m_ActiveKeys.Remove(key); }

        public static bool IsKeyPressed(Key key) => m_ActiveKeys.Contains(key);
        public static bool IsKeyPressed() => m_ActiveKeys.Count > 0;

        public static void Press(Button button) { ButtonDown?.Invoke(button); m_ActiveButtons.Add(button); }
        public static void Release(Button button) { ButtonUp?.Invoke(button); m_ActiveButtons.Remove(button); }

        public static bool IsButtonPressed(Button button) => m_ActiveButtons.Contains(button);
        public static bool IsButtonPressed() => m_ActiveButtons.Count > 0;

        public static void BLModifyMousePosition(Vector2 newPosition) { MousePosition = newPosition; }
        public static void BLSetWorldProperties(Vector2? windowSize = null, Vector2? cameraPosition = null)
        { m_WindowSize = windowSize ?? m_WindowSize; m_CameraPosition = cameraPosition ?? m_CameraPosition; }
    }
}
