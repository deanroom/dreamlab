using System;
using System.Threading;

namespace Threading
{
    public class WaitOneTest
    {
        static ManualResetEvent autoEvent = new ManualResetEvent(false);

        public static void Run()
        {
            Console.WriteLine("Main starting.");

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(WorkMethod), autoEvent);

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(WorkMethod), autoEvent);

            // Wait for work method to signal.
            autoEvent.WaitOne();
            Console.WriteLine("Work method signaled.\nMain ending.");
        }

        static void WorkMethod(object stateInfo) 
        {
            Console.WriteLine("Work starting.");

            // Simulate time spent working.
            Thread.Sleep(5000);

            // Signal that work is finished.
            Console.WriteLine("Work ending.");
            ((ManualResetEvent)stateInfo).Set();
        }
    }
}