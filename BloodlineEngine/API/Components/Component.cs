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

        public Transform Transform { get => Root.Transform; set => Root.Transform = value; }
        public Component Pos(Vector2 position) { Transform.Position = position; return this; }
        public Component Ctr(Vector2 center) { Transform.Center = center; return this; }
        public Component Scl(Vector2 scale) { Transform.Scale = scale; return this; }
        public Component Rot(float rotation) { Transform.Rotation = rotation; return this; }
        public Component Zee(float z) { Transform.Z = z; return this; }

        public Component()
        {
            BLGeneralComponentHandler.AddGlobalComponent(this);
            BLDebugAwake();
        }

        ~Component()
        {
            BLGeneralComponentHandler.RemoveGlobalComponent(this);
            BLKilled();
        }

        public void SetRoot(Root root) { Root = root; BLAwake(); }

        public BLArgStorage Args => Root.Args;
        public Component Arg(string key, object value) { Args[key] = value; return this; }

        public bool IsActive { get; private set; } = true;
        public void Enable()
        {
            if (IsActive) { Debug.BLWarn("Enabling a component that is already active!"); }
            IsActive = true;
            BLEnabled();
        }
        public void Disable()
        {
            if (!IsActive) { Debug.BLWarn("Disabling a component that is already not active!"); }
            IsActive = false;
            BLDisabled();
        }

        public delegate void BLComponentEventDelegate(Component component);
        public static BLComponentEventDelegate ReadyDelegate = (Component component) => component.BLReady();
        public static BLComponentEventDelegate DebugSparkDelegate = (Component component) => component.BLDebugSpark();
        public static BLComponentEventDelegate SparkDelegate = (Component component) => component.BLSpark();
        public static BLComponentEventDelegate DrawDelegate = (Component component) => component.BLDraw();
        public static BLComponentEventDelegate UpdateDelegate = (Component component) => component.BLUpdate();
        public static BLComponentEventDelegate FixedUpdateDelegate = (Component component) => component.BLFixedUpdate();
        public static BLComponentEventDelegate DebugShiftDelegate = (Component component) => component.BLDebugShift();
        public static BLComponentEventDelegate HaltDelegate = (Component component) => component.BLHalt();

        private bool m_DebugSparkOnce, m_SparkOnce, m_DrawOnce, m_UpdateOnce, m_FixedUpdateOnce, m_DebugShiftOnce = false;

        public virtual void Ready() { }
        public virtual void DebugSpark() { }
        public virtual void Spark() { }
        public virtual void Draw() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void DebugShift() { }
        public virtual void Halt() { }

        public virtual void DebugSparkOnce() { }
        public virtual void SparkOnce() { }
        public virtual void DrawOnce() { }
        public virtual void UpdateOnce() { }
        public virtual void FixedUpdateOnce() { }
        public virtual void DebugShiftOnce() { }

        public virtual void Awake() { }
        public virtual void DebugAwake() { }
        public virtual void Enabled() { }
        public virtual void Killed() { }
        public virtual void Disabled() { }

        protected virtual void BaseReady() { }
        protected virtual void BaseDebugSpark() { }
        protected virtual void BaseSpark() { }
        protected virtual void BaseDraw() { }
        protected virtual void BaseUpdate() { }
        protected virtual void BaseFixedUpdate() { }
        protected virtual void BaseDebugShift() { }
        protected virtual void BaseHalt() { }

        protected virtual void BaseDebugSparkOnce() { }
        protected virtual void BaseSparkOnce() { }
        protected virtual void BaseDrawOnce() { }
        protected virtual void BaseUpdateOnce() { }
        protected virtual void BaseFixedUpdateOnce() { }
        protected virtual void BaseDebugShiftOnce() { }

        protected virtual void BaseAwake() { }
        protected virtual void BaseDebugAwake() { }
        protected virtual void BaseEnabled() { }
        protected virtual void BaseKilled() { }
        protected virtual void BaseDisabled() { }

        private void BLReady() { BaseReady(); Ready(); Debug.Assert(Root is not null, "Component root is null!"); }
        private void BLDebugSpark() {
            BaseDebugSpark(); DebugSpark();
            if (!m_DebugSparkOnce) { BaseDebugSparkOnce(); DebugSparkOnce(); m_DebugSparkOnce = true; }
        }
        private void BLSpark() {
            BaseSpark(); Spark();
            if (!m_SparkOnce) { BaseSparkOnce(); SparkOnce(); m_SparkOnce = true; }
        }
        private void BLDraw() {
            BaseDraw(); Draw();
            if (!m_DrawOnce) { BaseDrawOnce(); DrawOnce(); m_DrawOnce = true; }
        }
        private void BLUpdate() {
            BaseUpdate(); Update();
            if (!m_UpdateOnce) { BaseUpdateOnce(); UpdateOnce(); m_UpdateOnce = true; }
        }
        private void BLFixedUpdate() {
            BaseFixedUpdate(); FixedUpdate();
            if (!m_FixedUpdateOnce) { BaseFixedUpdateOnce(); FixedUpdateOnce(); m_FixedUpdateOnce = true; }
        }
        private void BLDebugShift() {
            BaseDebugShift(); DebugShift();
            if (!m_DebugShiftOnce) { BaseDebugShiftOnce(); DebugShiftOnce(); m_DebugShiftOnce = true; }
        }
        private void BLHalt() { BaseHalt(); Halt(); }

        private void BLAwake() { BaseAwake(); Awake(); }
        private void BLDebugAwake() { BaseDebugAwake(); DebugAwake(); }
        private void BLEnabled() { BaseEnabled(); Enabled(); }
        private void BLKilled() { BaseKilled(); Killed(); }
        private void BLDisabled() { BaseDisabled(); Disabled(); }

        public override string ToString() { return "Component()"; }
    }
}
