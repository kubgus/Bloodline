namespace BloodlineEngine
{
    public abstract class BLApplication
    {
        public BLWindow Window { get; private set; }
        public BLRenderer Renderer => Window.Renderer;

        public Camera RenderedCamera => Renderer.Camera;
        public Camera RendererCamera => Renderer.Camera;

        private bool m_Running;
        private Thread m_MainThread;

        public BLApplication(Vector2 windowSize, string windowTitle = "Bloodline Application",
            bool lauchWithConsoleShown = false, bool windowResizable = false)
        {
            if (lauchWithConsoleShown) Debug.StartConsole();

            Window = new BLWindow(windowSize, windowTitle, windowResizable);

            m_MainThread = new Thread(MainLoop);
            m_MainThread.Start();

            Time.FixedUpdate += BLFixedUpdate;

            m_Running = true;
        }

        private void MainLoop()
        {
            BLReady();

            while (m_Running)
            {
                BLDebugSpark();

                if (!m_MainThread.IsAlive || !Window.Opened) { m_Running = false; return; }

                BLSpark();

                try
                {
                    BLDraw();

                    Renderer.Render();

                    BLUpdate();

                    Thread.Sleep(1);
                }
                catch (Exception error) { 
                    Debug.BLWarn("Window is not present! It is either being initialized or has not been created at all.");
                    Debug.BLError(error);
                }

                BLDebugShift();
            }

            BLHalt();

            return; // Formerly Application.Exit()
        }

        private Dictionary<string, object> m_NamedInstances = new();
        private List<object> m_UnnamedInstances = new();
        protected T Instantiate<T>(string key) where T : Root, new()
        {
            Debug.Assert(!m_NamedInstances.ContainsKey(key), $"An instance with key: '{key}' already exists!");
            T instance = new();
            m_NamedInstances[key] = instance;
            return instance;
        }
        protected T Instantiate<T>() where T : Root, new()
        {
            Debug.Assert(!m_UnnamedInstances.OfType<T>().Any(), $"An instance of this class already exists. Give it a key!");
            T instance = new();
            m_UnnamedInstances.Add(instance);
            return instance;
        }
        protected static T ThrowawayInstance<T>() where T : Root, new() { return new T(); }
        protected T GetInstance<T>(string key) where T : Root, new() { return (T)m_NamedInstances[key]; }
        protected object GetInstance(string key) { return m_NamedInstances[key]; }
        protected T GetInstance<T>() where T : Root, new() { return m_UnnamedInstances.OfType<T>().First(); }

        public virtual void Ready() { } // Runs when the GameLoop starts
        public virtual void DebugSpark() { } // Runs before everything else
        public virtual void Spark() { } // Runs after error checks before everything else
        public virtual void Draw() { } // Runs before drawing after Window is confirmed
        public virtual void Update() { } // Runs after drawing
        public virtual void FixedUpdate() { } // Runs 60 times per second
        public virtual void DebugShift() { } // Runs directly before the next frame's DebugTick
        public virtual void Halt() { } // Runs when the GameLoop ends

        private void BLReady()
        {
            Ready(); BLGeneralComponentHandler.Run(Component.ReadyDelegate);
            Time.ResetTime();
        }
        private void BLDebugSpark()
        {
            DebugSpark(); BLGeneralComponentHandler.Run(Component.DebugSparkDelegate);
            Time.BLNextFrame();
        }
        private void BLSpark()
        { Spark(); BLGeneralComponentHandler.Run(Component.SparkDelegate); }
        private void BLDraw()
        { Draw(); BLGeneralComponentHandler.Run(Component.DrawDelegate); }
        private void BLUpdate()
        { Update(); BLGeneralComponentHandler.Run(Component.UpdateDelegate); }
        private void BLFixedUpdate(object? sender, EventArgs e)
        { FixedUpdate(); BLGeneralComponentHandler.Run(Component.FixedUpdateDelegate); }
        private void BLDebugShift()
        { DebugShift(); BLGeneralComponentHandler.Run(Component.DebugShiftDelegate); }
        private void BLHalt()
        { Halt(); BLGeneralComponentHandler.Run(Component.HaltDelegate); }
    }
}