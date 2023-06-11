namespace BloodlineEngine
{
    public abstract class Component : IBLToString
    {
        public Root Root
        {
            get => m_ActiveRoot ?? null; // TODO: Figure out something to replace null with.
            set => m_ActiveRoot = value;
        }

        private Root? m_ActiveRoot;

        public delegate void BLComponentEventDelegate(Component component);
        public static BLComponentEventDelegate ReadyDelegate = (Component component) => component.BLReady();
        public static BLComponentEventDelegate DebugSparkDelegate = (Component component) => component.BLDebugSpark();
        public static BLComponentEventDelegate SparkDelegate = (Component component) => component.BLSpark();
        public static BLComponentEventDelegate DrawDelegate = (Component component) => component.BLDraw();
        public static BLComponentEventDelegate UpdateDelegate = (Component component) => component.BLUpdate();
        public static BLComponentEventDelegate FixedUpdateDelegate = (Component component) => component.BLFixedUpdate();
        public static BLComponentEventDelegate DebugShiftDelegate = (Component component) => component.BLDebugShift();
        public static BLComponentEventDelegate HaltDelegate = (Component component) => component.BLHalt();

        public Component()
        {
            BLGeneralComponentHandler.AddGlobalComponent(this);
        }

        ~Component()
        {
            BLGeneralComponentHandler.RemoveGlobalComponent(this);
        }

        public virtual void Ready() { }
        public virtual void DebugSpark() { }
        public virtual void Spark() { }
        public virtual void Draw() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void DebugShift() { }
        public virtual void Halt() { }

        private void BLReady() { Ready(); Debug.Assert(Root is not null, "Component root is null!"); }
        private void BLDebugSpark() { DebugSpark(); }
        private void BLSpark() { Spark(); }
        private void BLDraw() { Draw(); }
        private void BLUpdate() { Update(); }
        private void BLFixedUpdate() { FixedUpdate(); }
        private void BLDebugShift() { DebugShift(); }
        private void BLHalt() { Halt(); }

        public override string ToString() { return "Component()"; }
    }
}
