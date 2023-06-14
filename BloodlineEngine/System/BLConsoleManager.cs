using System.Runtime.InteropServices;

namespace BloodlineEngine
{
    public static class BLConsoleManager
    {
        public static bool IsConsoleOpen { get; private set; }

        private static StreamWriter m_NullWriter = new(Stream.Null);

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        // TODO: Add ToggleConsole().

        public static void OpenConsole()
        { if (!IsConsoleOpen) StartConsole(); }
        public static void StartConsole()
        {
            AllocConsole();

            Stream stream = Console.OpenStandardOutput();
            StreamWriter writer = new StreamWriter(stream);
            Console.SetOut(writer);

            IsConsoleOpen = true;
        }

        public static void CloseConsole()
        { if (IsConsoleOpen) KillConsole(); }
        public static void KillConsole()
        {
            FreeConsole();

            Console.SetOut(m_NullWriter);

            IsConsoleOpen = false;
        }
    }
}
