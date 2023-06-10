namespace BloodlineEngine
{
    public abstract class Root
    {
        public Transform Transform { get; set; }

        private List<Component> m_LocalActiveComponents = new();

        public Root()
        {
            Transform = AddComponent<Transform>();
        }

        public T GetComponent<T>() where T : Component, new()
        {
            T? component = (T?)m_LocalActiveComponents.Find(x => x is T);
            Debug.Assert(component is not null, "This component doesn't exist!");
            return component ?? AddComponent<T>();
        }

        public T AddComponent<T>() where T : Component, new()
        {
            Debug.Assert(m_LocalActiveComponents.Find(x => x is T) is null, "This component already exists!");

            T component = new() { Root = this };
            m_LocalActiveComponents.Add(component);
            return component;
        }
    }
}
