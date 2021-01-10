using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadingTest
{
    class ThreadingContextTest
    {
        public static void Run(){
            Console.WriteLine(Stopwatch.GetTimestamp());
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000_0000; i++)
            {
                //Thread.Sleep(0);
            }
            Console.WriteLine($"Cost time {sw.ElapsedMilliseconds}ms");
            Console.WriteLine(Stopwatch.GetTimestamp());
        }

    }
}
