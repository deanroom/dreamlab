using System;
using System.Threading;

namespace ThreadingTest
{
    public class ThreadTest
    {
        public static void Run()
        {
            Console.WriteLine("begin.");
            var thread = new Thread(new ThreadStart(RunThread));
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            Console.WriteLine("finish.");
        }

        private static void RunThread()
        {
            Console.WriteLine("thread begin.");
            Thread.Sleep(2000);
            Console.WriteLine("thread,end"); 
        }
    }
}