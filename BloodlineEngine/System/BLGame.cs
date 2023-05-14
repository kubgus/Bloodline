using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodlineEngine
{
    public abstract class BLGame
    {
        public BLWindow Window { get; private set; }

        private bool m_Running;

        public BLGame()
        {
            Window = new BLWindow();

            Debug.Trace("Everything is.");
            Debug.BLInfo(new Vector2(55f, 44f));
            Debug.BLWarn("You are using outdated dependencies!");
            Debug.Error("Window wasn't initialized properly!");

            m_Running = true;
            while (m_Running)
            {

            }
        }
    }
}