namespace BloodlineEngine
{
    public static class Chance
    {
        private static Random m_RandomClass = new();

        public static float RandomFloat(float maxExclusive) { return RandomFloat(maxExc: maxExclusive); }
        public static float RandomFloat(float? minInc = null, float? maxExc = null)
        {
            float maxVal = maxExc ?? 1f;
            float minVal = minInc ?? 0f;
            if (minVal > maxVal) 
            { Debug.BLWarn("A Chance class method used with higher minimum value than maximum value! This might not work as expected."); }

            float random = (float)m_RandomClass.NextDouble() * (maxVal - minVal) + minVal;
            return random;
        }

        public static int RandomInt(int maxExclusive) { return RandomInt(maxExc: maxExclusive); }
        public static int RandomInt(int? minInc = null, int? maxExc = null)
        {
            return (int)RandomFloat(minInc, maxExc);
        }

        public static int AnyInt() { return m_RandomClass.Next(); }

        public static T Pick<T>(T[] values)
        {
            if (values == null || values.Length == 0)
            { throw new ArgumentException("The input array is empty or null."); }
            return values[RandomInt(0, values.Length)];
        }
    }
}
