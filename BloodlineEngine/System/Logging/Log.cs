using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodlineEngine
{
    public static class Log
    {
        public static void Write(dynamic value)
        {
            Console.WriteLine(value);
        }

        public static void Write(ILoggable value)
        {
            Console.WriteLine(value.ToString());
        }
    }
}
