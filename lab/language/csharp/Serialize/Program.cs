using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TypeNamingTest.Run();
            Console.WriteLine("Cost Time {0}ms",sw.ElapsedMilliseconds);
            sw.Restart();
            BoisTest.Run();
            Console.WriteLine("Cost Time {0}ms",sw.ElapsedMilliseconds);
            
        }
    }
}