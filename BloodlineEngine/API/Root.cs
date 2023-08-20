using System.Net.Mail;

namespace BloodlineEngine
{
    public abstract class Root
    {
        public Transform Transform
        {
            get => m_TransformComponent.Transform;
            set => m_TransformComponent.Transform = value;
        }
        // TODO: Check if not being able to access TransformComponent causes any problems.
        private BLTransformComponent m_TransformComponent;

        private List<Component> m_LocalActiveComponents = new();

        public Root()
        {
            m_TransformComponent = CreateComponent<BLTransformComponent>();
        }

        public T GetComponent<T>() where T : Component, new()
        {
            T? component = (T?)m_LocalActiveComponents.Find(x => x is T);
            Debug.Assert(component is not null, "This component doesn't exist!");
            return component ?? CreateComponent<T>();
        }

        /// <returns>The created component.</returns>
        public T CreateComponent<T>() where T : Component, new()
        {
            Debug.Assert(m_LocalActiveComponents.Find(x => x is T) is null, "This component already exists!");

            T component = new() { Root = this };
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
