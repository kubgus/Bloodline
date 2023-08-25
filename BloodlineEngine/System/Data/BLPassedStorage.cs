namespace BloodlineEngine
{
    public class BLPassedStorage
    {
        protected Dictionary<string, object> m_PassedValues = new();

        public object this[string key]
        {
            get => m_PassedValues[key];
            set => m_PassedValues[key] = value;
        }

        public BLPassedStorage Pass(string key, object value) { this[key] = value; return this; }
        public BLPassedStorage Pass<T>(string key, T value) where T : notnull { this[key] = value; return this; }
        protected object Await(string key) { Debug.Assert(m_PassedValues.ContainsKey(key), "Await failed! Property not passed!"); return this[key]; }
        protected T Await<T>(string key) { return (T)Await(key); }
    }
}
