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
        public static BLComponentEventDelegate DebugTickDelegate = (Component component) => component.BLDebugTick();
        public static BLComponentEventDelegate TickDelegate = (Component component) => component.BLTick();
        public static BLComponentEventDelegate StepDelegate = (Component component) => component.BLStep();
        public static BLComponentEventDelegate UpdateDelegate = (Component component) => component.BLUpdate();
        public static BLComponentEventDelegate DebugFrameDelegate = (Component component) => component.BLDebugFrame();
        public static BLComponentEventDelegate FinishDelegate = (Component component) => component.BLFinish();

        public Component()
        {
            BLGeneralComponentHandler.AddGlobalComponent(this);
        }

        ~Component()
        {
            BLGeneralComponentHandler.RemoveGlobalComponent(this);
        }

        public virtual void Ready() { }
        public virtual void DebugTick() { }
        public virtual void Tick() { }
        public virtual void Step() { }
        public virtual void Update() { }
        public virtual void DebugFrame() { }
        public virtual void Finish() { }

        private void BLReady() { Ready(); Debug.Assert(Root is not null, "Component root is null!"); }
        private void BLDebugTick() { DebugTick(); }
        private void BLTick() { Tick(); }
        private void BLStep() { Step(); }
        private void BLUpdate() { Update(); }
        private void BLDebugFrame() { DebugFrame(); }
        private void BLFinish() { Finish(); }

        public override string ToString() { return "Component()"; }
    }
}
