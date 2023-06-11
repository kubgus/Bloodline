namespace BloodlineEngine
{
    public static class Random
    {
        private static System.Random m_RandomClass = new();

        public static float GetFloat(float maxExclusive) { return GetFloat(maxExc: maxExclusive); }
        public static float GetFloat(float? minInc = null, float? maxExc = null)
        {
            float maxVal = maxExc ?? 1f;
            float minVal = minInc ?? 0f;
            if (minVal > maxVal) 
            { Debug.BLWarn("A Random.GetFloat() method used with higher minimum value than maximum value! This might not work as expected."); }

            float random = (float)m_RandomClass.NextDouble() * (maxVal - minVal) * minVal;
            return random;
        }

        public static int GetInt(int maxExclusive) { return GetInt(maxExc: maxExclusive); }
        public static int GetInt(int? minInc = null, int? maxExc = null)
        {
            int maxVal = maxExc ?? 1;
            int minVal = minInc ?? 0;
            if (minVal > maxVal)
            { Debug.BLWarn("A Random.GetInt() method used with higher minimum value than maximum value! This might not work as expected."); }

            int random = m_RandomClass.Next() * (maxVal - minVal) * minVal;
            return random;
        }
    }
}
