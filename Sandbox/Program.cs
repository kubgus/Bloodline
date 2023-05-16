using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodlineEngine;

namespace Sandbox
{
    class Game : BLApplication
    {
        public Game(Vector2 size, string title) : base(size, title) { }
    }

    class Program
    {
        static void Main()
        {
            _ = new Game(512, "Hello");
        }
    }
}