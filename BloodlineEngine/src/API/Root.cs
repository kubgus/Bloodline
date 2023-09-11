namespace BloodlineEngine
{
    class BLRootTransformComponent : BLTransformComponent
    {
        public override void UpdateOnce() { Root.Init(); }
    }

    public abstract class Root
    {
        public Transform Transform
        {
            get => m_TransformComponent.Transform;
            set => m_TransformComponent.Transform = value;
        }
        private BLTransformComponent m_TransformComponent;

        private List<Component> m_LocalActiveComponents = new();

        public Root()
        {
            m_TransformComponent = CreateComponent<BLRootTransformComponent>();
        }

        public BLArgStorage Args = new();
        public Root Arg(string key, object value) { Args[key] = value; return this; }
       
        /// <summary>
        /// Create components inside this method.
        /// </summary>
        public virtual void Init() { }

        public void Enable() { foreach (Component component in m_LocalActiveComponents) { component.Enable(); } }
        public void Disable() { foreach (Component component in m_LocalActiveComponents) { component.Disable(); } }

        public T GetComponent<T>() where T : Component, new()
        {
            T? component = (T?)m_LocalActiveComponents.Find(x => x is T);
            Debug.Assert(component is not null, "This component doesn't exist!");
            return component ?? CreateComponent<T>();
        }

        /// <returns>The created component.</returns>
        public T CreateComponent<T>() where T : Component, new()
        {
            Debug.Assert(m_LocalActiveComponents.Find(x => x is T) is null, $"This component already exists: { typeof(T) }");

            T component = new();
            component.SetRoot(this);
            m_LocalActiveComponents.Add(component);
            return component;
        }

        /// <param name="altered">Change to false to return the object before modification.</param>
        /// <returns>The object before or after modification.</returns>
        public Root AddComponent<T>(bool altered = true) where T : Component, new()
        {
            Root before = this;
            CreateComponent<T>();
            return altered ? this : before;
        }
    }
}
