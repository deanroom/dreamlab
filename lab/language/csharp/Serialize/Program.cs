using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Jil;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace serialize
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            var timeCost = new List<long>();
            for (int i = 0; i < 2000; i++)
            {
                sw.Restart();
                TypeNamingTest.Run();
                timeCost.Add(sw.ElapsedTicks);
                //Console.WriteLine($"Max time {timeCost.Max(x=>x).ToString()}, avg {timeCost.Average(x=>x).ToString()}");
            }

            var top = timeCost.OrderByDescending(x => x).Take(100);
var index = 0;
            foreach (var time in timeCost.OrderBy(x=>x))
            {
                index++;
                Console.WriteLine($"{index},{time}");
            }
        }
    }
}