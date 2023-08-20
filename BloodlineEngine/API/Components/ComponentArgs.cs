namespace BloodlineEngine
{
    public class ComponentArgs
    {
        public string Tag { get; set; }

        private Dictionary<string, object> m_CustomArgs = new Dictionary<string, object>();

        public ComponentArgs(string tag = "Component") { Tag = tag; }

        public int Count => m_CustomArgs.Count;

        public object this[string key] 
        {
            get => m_CustomArgs[key];
            set => m_CustomArgs[key] = value;
        }

        public T Get<T>(string key) { return (T)m_CustomArgs[key]; }
        public ComponentArgs Set(Dictionary<string, object> customArgs) { m_CustomArgs = customArgs; return this; }
        public ComponentArgs Copy() { return new ComponentArgs().Set(m_CustomArgs); }
        public IEnumerable<string> GetKeys() { return m_CustomArgs.Keys; }
        public bool Contains(string key) { return m_CustomArgs.ContainsKey(key); }
        public bool Remove(string key) { return m_CustomArgs.Remove(key); }
        public void Clear() { m_CustomArgs.Clear(); }
    }
}
