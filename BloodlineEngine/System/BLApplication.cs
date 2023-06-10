namespace BloodlineEngine
{
    public abstract class BLApplication
    {
        public BLWindow Window { get; private set; }

        private bool m_Running;
        private Thread m_MainThread;

        public BLApplication(Vector2 windowSize, string windowTitle = "Bloodline Application", bool windowResizable = false)
        {
            Window = new BLWindow(windowSize, windowTitle, windowResizable);

            m_MainThread = new Thread(MainLoop);
            m_MainThread.Start();

            m_Running = true;

            Application.Run(Window);
        }

        void MainLoop()
        {
            BLReady();

            while (m_Running)
            {
                BLDebugTick();

                if (!m_MainThread.IsAlive || !Window.Opened) { m_Running = false; return; }

                BLTick();

                try
                {
                    BLStep();

                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });

                    BLUpdate();

                    Thread.Sleep(1);
                }
                catch { Debug.BLWarn("Window is not present! It is either being initialized or has not been created at all."); }

                BLDebugFrame();
            }

            BLFinish();

            Application.Exit();
        }

        public virtual void Ready() { } // Runs when the GameLoop starts
        public virtual void DebugTick() { } // Runs before everything else
        public virtual void Tick() { } // Runs after error checks before everything else
        public virtual void Step() { } // Runs before drawing after Window is confirmed
        public virtual void Update() { } // Runs after drawing
        public virtual void DebugFrame() { } // Runs directly before the next frame's DebugTick
        public virtual void Finish() { } // Runs when the GameLoop ends

        private void BLReady() { Ready(); BLGeneralComponentHandler.Run(Component.ReadyDelegate); }
        private void BLDebugTick() { DebugTick(); BLGeneralComponentHandler.Run(Component.DebugTickDelegate); }
        private void BLTick() { Tick(); BLGeneralComponentHandler.Run(Component.TickDelegate); }
        private void BLStep() { Step(); BLGeneralComponentHandler.Run(Component.StepDelegate); }
        private void BLUpdate() { Update(); BLGeneralComponentHandler.Run(Component.UpdateDelegate); }
        private void BLDebugFrame() { DebugFrame(); BLGeneralComponentHandler.Run(Component.DebugFrameDelegate); }
        private void BLFinish() { Finish(); BLGeneralComponentHandler.Run(Component.FinishDelegate); }

    }
}