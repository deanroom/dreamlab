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
            for (int i = 0; i < 1_00_000; i++)
            {
                TypeNamingTest.Run();
            }
            Console.WriteLine($"Cost time {sw.ElapsedMilliseconds}ms");
            
            
        }
    }
}