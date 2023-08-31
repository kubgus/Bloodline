namespace BloodlineEngine
{
    public class BLArgStorage
    {
        private Dictionary<string, object> m_Args = new();

        public object this[string key]
        {
            get => Use(key);
            set => Store(key, value);
        }

        public object Use(string key)
        {
            Debug.Assert(m_Args.ContainsKey(key), $"Requested arg: '{key}' wasn't found!");
            return m_Args[key];
        }
        public T Use<T>(string key) { return (T)Use(key); }

        public BLArgStorage Store(string key, object value) { m_Args[key] = value; return this; }
    }
}
