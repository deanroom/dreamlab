using System;
using System.Threading;

namespace ThreadingTest
{
    public class NormalThread
    {
        public static void Run()
        {
            Thread t = new Thread(Worker);
            t.IsBackground=true;
            t.Start();
            
        }
        private static void Worker()
        {
            while(true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Thread run.");

            }
        }
    }

}