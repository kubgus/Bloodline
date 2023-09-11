using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace BloodlineEngine
{
    public static class Debug
    {
        public static bool IsConsoleOpen { get; private set; }

        private static StreamWriter m_NullWriter = new(Stream.Null);

        public static void ToggleConsole()
        {
            if (IsConsoleOpen) { KillConsole(); }
            else if (!IsConsoleOpen) { OpenConsole(); }
        }

        public static void OpenConsole()
        { if (!IsConsoleOpen) StartConsole(); }
        public static void StartConsole()
        {
            AllocConsole();

            Stream stream = Console.OpenStandardOutput();
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
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

        public static void Trace(dynamic value) { Console.ForegroundColor = ConsoleColor.White; Log("CL", 'T', value); }
        public static void Info(dynamic value) { Console.ForegroundColor = ConsoleColor.Green; Log("CL", 'I', value); }
        public static void Warn(dynamic value) { Console.ForegroundColor = ConsoleColor.Yellow; Log("CL", 'W', value); }
        public static void Error(dynamic value) { Console.ForegroundColor = ConsoleColor.Red; Log("CL", 'E', value); }

        public static void BLTrace(dynamic value) { Console.ForegroundColor = ConsoleColor.White; Log("BL", 'T', value); }
        public static void BLInfo(dynamic value) { Console.ForegroundColor = ConsoleColor.Green; Log("BL", 'I', value); }
        public static void BLWarn(dynamic value) { Console.ForegroundColor = ConsoleColor.Yellow; Log("BL", 'W', value); }
        public static void BLError(dynamic value) { Console.ForegroundColor = ConsoleColor.Red; Log("BL", 'E', value); }


        /// <summary>
        /// Throw error if condition is false.
        /// </summary>
        /// <param name="message">Error message</param>
        public static void Assert(bool condition, string message)
        {
            if (condition) return;

            StackTrace stackTrace = new(true);
            StackFrame? frame = stackTrace.GetFrame(1) ?? throw new BLAssertException(message);
            string? path = frame.GetFileName() ?? throw new BLAssertException(message);
            int line = frame.GetFileLineNumber();

            throw new BLAssertException(message, path, line);
        }

        class BLAssertException : Exception
        {
            public BLAssertException(string message, string path = "not found", int line = 0)
                : base($"{message} ({path}:{line})") { }
        }

        private static void Log(string caller, char code, dynamic value)
        { 
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("[");
            stringBuilder.Append(DateTime.Now.ToString("HH:mm:ss.fff"));
            stringBuilder.Append("] (");
            stringBuilder.Append(caller);
            stringBuilder.Append(code);
            stringBuilder.Append(") ");
            stringBuilder.Append(value);
            stringBuilder.AppendLine();

            string final = stringBuilder.ToString();
            Console.Write(final);
            Console.Out.Flush();
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
