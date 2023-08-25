namespace BloodlineEngine
{
    public abstract class Component : BLPassedStorage, IBLToString
    {
        public Root Root
        {
            get => m_ActiveRoot ?? null; // TODO: Figure out something to replace null with.
            set => m_ActiveRoot = value;
        }
        public Transform Transform { get => Root.Transform; set => Root.Transform = value; }
        public Component Pos(Vector2 position) { Transform.Position = position; return this; }
        public Component Ctr(Vector2 center) { Transform.Center = center; return this; }
        public Component Scl(Vector2 scale) { Transform.Scale = scale; return this; }
        public Component Rot(float rotation) { Transform.Rotation = rotation; return this; }

        public bool IsActive { get; private set; } = true;
        public void Enable() { 
            if (IsActive) { Debug.BLWarn("Enabling a component that is already active!"); }
            IsActive = true; 
        }
        public void Disable()
        {
            if (!IsActive) { Debug.BLWarn("Disabling a component that is already not active!"); }
            IsActive = false;
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

        protected virtual void BaseReady() { }
        protected virtual void BaseDebugSpark() { }
        protected virtual void BaseSpark() { }
        protected virtual void BaseDraw() { }
        protected virtual void BaseUpdate() { }
        protected virtual void BaseFixedUpdate() { }
        protected virtual void BaseDebugShift() { }
        protected virtual void BaseHalt() { }

        private void BLReady() { BaseReady(); Ready(); Debug.Assert(Root is not null, "Component root is null!"); }
        private void BLDebugSpark() { BaseDebugSpark(); DebugSpark(); }
        private void BLSpark() { BaseSpark(); Spark(); }
        private void BLDraw() { BaseDraw(); Draw(); }
        private void BLUpdate() { BaseUpdate(); Update(); }
        private void BLFixedUpdate() { BaseFixedUpdate(); FixedUpdate(); }
        private void BLDebugShift() { BaseDebugShift(); DebugShift(); }
        private void BLHalt() { BaseHalt(); Halt(); }

        public override string ToString() { return "Component()"; }
    }
}
