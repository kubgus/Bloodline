using static BloodlineEngine.Component;

namespace BloodlineEngine
{
    public static class BLGeneralComponentHandler
    {
        private static List<Component> m_ActiveComponents = new();

        public static List<Component> Get()
        {
            return m_ActiveComponents.ToList();
        }

        public static void Run(BLComponentEventDelegate eventDelegate)
        {
            List<Component> queue = Get();

            foreach (Component component in queue)
            {
                eventDelegate(component);
            }
        }

        public static void AddGlobalComponent(Component component)
        {
            m_ActiveComponents.Add(component);
        }

        public static void RemoveGlobalComponent(Component component)
        {
            m_ActiveComponents.Remove(component);
        }
    }
}
