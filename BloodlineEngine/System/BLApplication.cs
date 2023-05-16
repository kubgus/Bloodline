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
            while (m_Running)
            {
                if (!m_MainThread.IsAlive || !Window.Opened) { m_Running = false; return; }

                try
                {
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });

                    Thread.Sleep(1);
                }
                catch { Debug.BLWarn("Window is not present! It is either being initialized or has not been created at all."); }
            }

            Application.Exit();
        }
    }
}