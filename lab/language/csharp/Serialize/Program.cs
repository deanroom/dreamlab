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
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                TypeNamingTest.Run();
                Console.WriteLine("Cost Time {0}ms",sw.ElapsedMilliseconds);
                sw.Restart();
                using (var output = new StringWriter())
                {
                    JSON.Serialize(new Me(){FirstName = i.ToString()},output);
                }
                Console.WriteLine("Cost Time {0}ms",sw.ElapsedMilliseconds);
                
                sw.Restart();
                Utf8Json.Run();
                Console.WriteLine("Cost Time {0}ms",sw.ElapsedMilliseconds);
            }
        }
    }
}