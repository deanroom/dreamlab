using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadingTest
{
    class ThreadingContextTest
    {
        public static void Run(){
            Console.WriteLine(Stopwatch.GetTimestamp());
            Thread.Sleep(0);
            Console.WriteLine(Stopwatch.GetTimestamp());
        }

    }
}
