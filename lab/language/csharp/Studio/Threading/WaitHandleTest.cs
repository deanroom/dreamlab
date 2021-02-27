using System;
using System.Threading;
namespace ThreadingTest
{
    public class WaithandleTest
    {
        static WaitHandle[] waitHandles = new WaitHandle[]
       {
        new AutoResetEvent(false),
        new AutoResetEvent(false)
       };

        // Define a random number generator for testing.
        static Random r = new Random();

        public static void Run()
        {
            // Queue up two tasks on two different threads;
            // wait until all tasks are completed.
            DateTime dt = DateTime.Now;
            Console.WriteLine("Main thread is waiting for BOTH tasks to complete.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[1]);
            WaitHandle.WaitAll(waitHandles);
            // The time shown below should match the longest task.
            Console.WriteLine("Both tasks are completed (time waited={0})",
                (DateTime.Now - dt).TotalMilliseconds);

            // Queue up two tasks on two different threads;
            // wait until any tasks are completed.
            dt = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine("The main thread is waiting for either task to complete.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[0]);
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoTask), waitHandles[1]);
            int index = WaitHandle.WaitAny(waitHandles);
            // The time shown below should match the shortest task.
            Console.WriteLine("Task {0} finished first (time waited={1}).",
                index + 1, (DateTime.Now - dt).TotalMilliseconds);
            Console.ReadLine();
        }

        static void DoTask(Object state)
        {
            AutoResetEvent are = (AutoResetEvent)state;
            int time = 1000 * r.Next(2, 10);
            Console.WriteLine("Performing a task for {0} milliseconds.", time);
            Thread.Sleep(time);
            are.Set();
        }
    }
}
