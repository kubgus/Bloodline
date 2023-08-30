// TODO: Allow clients to create custom event callback.

namespace BloodlineEngine
{
    public static class Input
    {
        public static bool IsScrolling { get; private set; }
        public static Vector2 MousePosition { get; private set; } = new();
        public static Vector2 MousePositionWindow => MousePosition;
        public static Vector2 MousePositionWorld => MousePosition + m_CameraPosition;

        private static Vector2 m_WindowSize = new();
        private static Vector2 m_CameraPosition = new();

        private static HashSet<Keys> m_ActiveKeys = new();
        private static HashSet<MouseButtons> m_ActiveMouseButtons = new();

        public static void Press(Keys key) { m_ActiveKeys.Add(key); TryToggleDebug(key); }
        public static void Press(object? sender, PreviewKeyDownEventArgs e) { Press(e.KeyCode); }
        public static void Release(Keys key) { m_ActiveKeys.Remove(key); }
        public static void Release(object? sender, KeyEventArgs e) { Release(e.KeyCode); }

        public static void Press(MouseButtons mouseButton) { m_ActiveMouseButtons.Add(mouseButton); }
        public static void Press(object? sender, MouseEventArgs e) { Press(e.Button); }
        public static void Release(MouseButtons mouseButton) { m_ActiveMouseButtons.Remove(mouseButton); }
        public static void Release(object? sender, MouseEventArgs e) { Release(e.Button); }

        public static bool IsKeyPressed(Keys key)
        { return m_ActiveKeys.Contains(key); }
        public static bool IsMouseButtonPressed(MouseButtons mouseButton)
        { return m_ActiveMouseButtons.Contains(mouseButton); }

        public static void BLModifyMousePosition(object? sender, MouseEventArgs e) { MousePosition = (Vector2)e.Location; }
        public static void BLSetWorldProperties(Vector2? windowSize = null, Vector2? cameraPosition = null)
        { m_WindowSize = windowSize ?? m_WindowSize; m_CameraPosition = cameraPosition ?? m_CameraPosition; }

        private static void TryToggleDebug(Keys key) { if (key == Keys.F5) Debug.ToggleConsole(); }
    }
}
