using System.Diagnostics;

namespace BloodlineEngine
{
    public static class Time
    {
        public static float ElapsedMilliseconds { get => m_Stopwatch.ElapsedMilliseconds; }
        public static float ElapsedSeconds { get => (float)m_Stopwatch.Elapsed.TotalSeconds; }
        public static float ElapsedFrames { get; private set; }

        public static float DeltaTime { get => GetDeltaTimeMilliseconds() / 1000f; }
        public static float GetDeltaTimeMilliseconds()
        {
            float deltaTime = ElapsedMilliseconds - m_LastFrameTime;
            m_LastFrameTime = ElapsedMilliseconds;
            return deltaTime;
        }

        private static Stopwatch m_Stopwatch = new();
        private static float m_LastFrameTime;

        public static void ResetTime () { m_Stopwatch.Reset(); m_Stopwatch.Start(); }

        public static void BLNextFrame()
        {
            ElapsedFrames++;
        }
    }
}
