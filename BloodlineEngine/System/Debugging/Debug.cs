using System.Diagnostics;

namespace BloodlineEngine
{
    public static class Debug
    {
        public static void Trace(dynamic value) { Console.ForegroundColor = ConsoleColor.White; Log("CL", 'T', value); }
        public static void Info(dynamic value) { Console.ForegroundColor = ConsoleColor.Green; Log("CL", 'I', value); }
        public static void Warn(dynamic value) { Console.ForegroundColor = ConsoleColor.Yellow; Log("CL", 'W', value); }
        public static void Error(dynamic value) { Console.ForegroundColor = ConsoleColor.Red; Log("CL", 'E', value); }

        public static void BLTrace(dynamic value) { Console.ForegroundColor = ConsoleColor.White; Log("BL", 'T', value); }
        public static void BLInfo(dynamic value) { Console.ForegroundColor = ConsoleColor.Green; Log("BL", 'I', value); }
        public static void BLWarn(dynamic value) { Console.ForegroundColor = ConsoleColor.Yellow; Log("BL", 'W', value); }
        public static void BLError(dynamic value) { Console.ForegroundColor = ConsoleColor.Red; Log("BL", 'E', value); }

        private static void Log(string caller, char code, dynamic value)
        { Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] ({caller}{code}) {value}"); }

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
    }
}
